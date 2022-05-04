using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;

namespace AgenzieTrasporto
{
    public class Passegero
    {
        public int idPassegero;
        public string fullName;
        public string documento;
        public int cartaCredito;
        public int pin;
        public List<Biglietto> biglietti;

        public Passegero(int idPassegero,string fullName, string documento, int cartaCredito,int pin)
        { 
            this.idPassegero = idPassegero;
            this.fullName = fullName;
            this.documento = documento;
            this.cartaCredito = cartaCredito;
            this.pin = pin;
        }


        public void PagaBiglietto(int cartaCredito, int pin)
        { 
            this.cartaCredito = cartaCredito;
            this.pin = pin;
            Console.WriteLine("Pagamento avenuto con sucesso! \nGrazie mille per aver pagato il biglietto");
        }
       

    }
   
    public class Viaggio
    {
        public int idViaggio;  
        public Mezzo mezzo;
        public DateTime data;
        public Biglietto biglietto;
        public string traggito;
        public decimal prezzo;
        public string agenziaNazionaleNome;
    }
    public class Biglietto
    {
        public int idBiglietto;
        public string  codiceBiglietto;
        public Passegero passegero;
        public Mezzo mezzo;
        public DateTime dataCreazioneBiglietto;
        public Viaggio viaggio;
       
       
        
       
    }
    public class Controllore
    {
        public int idControllore;
        public string codiceControllore;
        public string fullName;
        public List<AgenziaNazionale> agenzia;
        public Mezzo mezzo;

        public void ControllaBigletto(string codiceBiglietto)
        {

        }

        //public bool ControllaValiditaBiglietto(Biglietto biglietto)
        //{    

        //    if (DateTime.Today > biglietto.viaggio.data)
        //        return true;
        //    else return false;
        //}
    }

    public abstract class AgenziaNazionale
    {
        public string nome;

        public abstract void CreaControllore(int id, string fullName, string codice);
        public abstract void CreaMezzo(int id, string codice, Dictionary<int,Passegero>postiPassegeri, Controllore controllore, List<Fermate>fermate);
        public abstract void AsociaControlloreAMezzo(Controllore controllore,Mezzo mezzo);
        

    }
    public class AgenziaTerrestre : AgenziaNazionale
    {
        
        public List<Controllore> controlloriTerrestre = new List<Controllore>();
        public List<Mezzo> mezziTerrestre = new List<Mezzo>();
        public List<Biglietto> bigliettiTerrestre = new List<Biglietto>();
        public List<Viaggio> viaggiTerrestre = new List<Viaggio>();
      

        public AgenziaTerrestre(string nome)
        {
            base.nome = nome;
        }
        public override void CreaControllore(int id, string fullName, string codice)
        { 
            Controllore controlloreTerrestre = new Controllore();
            controlloreTerrestre.idControllore = id;
            controlloreTerrestre.fullName = fullName;
            controlloreTerrestre.codiceControllore = codice;
            controlloriTerrestre.Add(controlloreTerrestre);

        }

        public override void CreaMezzo(int id, string codice, Dictionary<int, Passegero> postiPassegeri, Controllore controllore, List<Fermate> fermate)
        {   
            MezzoTerrestre treno = new MezzoTerrestre();
            treno.idMezzo = id;
            treno.codiceMezzo = codice;
            treno.postiPassegeri = postiPassegeri;
            treno.controllore = controllore;
            treno.fermate = fermate;
            mezziTerrestre.Add(treno);
        }
        public void SalvaBiglietto(Biglietto biglietto)
        {
            bigliettiTerrestre.Add(biglietto);
        }
        public void SalvaViaggio(Viaggio viaggio)
        {
            viaggiTerrestre.Add(viaggio);
        }

        public override void AsociaControlloreAMezzo(Controllore controllore, Mezzo mezzo)
        { }

    }
    public class AgenziaMaritimo : AgenziaNazionale, ICheckIn
    {

        public List<Controllore> controlloriMarittimi = new List<Controllore>();
        public List<Mezzo> mezziMarittimi = new List<Mezzo>();
        public List<Biglietto> bigliettiMarittimi = new List<Biglietto>();
        public List<Viaggio> viaggiMarittimi = new List<Viaggio>();
        public AgenziaMaritimo(string nome)
        {
            base.nome = nome;
        }
        public override void CreaControllore(int id, string fullName, string codice)
        {
            Controllore controlloreMarittimo = new Controllore();
            controlloreMarittimo.idControllore = id;
            controlloreMarittimo.fullName = fullName;
            controlloreMarittimo.codiceControllore = codice;
            controlloriMarittimi.Add(controlloreMarittimo);

        }
        public override void CreaMezzo(int id, string codice, Dictionary<int, Passegero> postiPassegeri, Controllore controllore, List<Fermate> fermate)
        {
            MezzoMaritimo nave = new MezzoMaritimo();
            nave.idMezzo = id;
            nave.codiceMezzo = codice;
            nave.postiPassegeri = postiPassegeri;
            nave.controllore = controllore;
            nave.fermate = fermate;
            mezziMarittimi.Add(nave);
        }

        public void SalvaBiglietto(Biglietto biglietto)
        {
            bigliettiMarittimi.Add(biglietto);
        }
        public void SalvaViaggio(Viaggio viaggio)
        {
            viaggiMarittimi.Add(viaggio);
        }
        public void CheckIn( string codiceBiglietto, Passegero passegero) 
        {
            var querry = from Biglietto in bigliettiMarittimi where Biglietto.codiceBiglietto == codiceBiglietto select Biglietto;
            foreach (Biglietto b in querry)
            {
                if (querry != null)
                {
                    Console.WriteLine("Codice biglietto trovato nella banca dati. CHECK IN AVENUTO CON SUCESSO!");
                }
                else
                {
                    Console.WriteLine("Codice non trovato non puoi fare check in.");
                }
            }
        }
        public override void AsociaControlloreAMezzo(Controllore controllore, Mezzo mezzo)
        { 

        }

    }
    public class AgenziaAereo : AgenziaNazionale, ICheckIn
    {
        public List<Controllore> controlloriAereo = new List<Controllore>();
        public List<Mezzo> mezziAereo = new List<Mezzo>();
        public List<Biglietto> bigliettiAerei = new List<Biglietto>();
        public List<Viaggio> viaggiAerei = new List<Viaggio>();

        public AgenziaAereo(string nome)
        {
            base.nome = nome;
        }
        public override void CreaControllore(int id, string fullName, string codice)
        {
            Controllore controlloreAereo = new Controllore();
            controlloreAereo.idControllore = id;
            controlloreAereo.fullName = fullName;
            controlloreAereo.codiceControllore = codice;
            controlloriAereo.Add(controlloreAereo);
        }
        public override void CreaMezzo(int id, string codice, Dictionary<int, Passegero> postiPassegeri, Controllore controllore, List<Fermate> fermate)
        {
            MezzoAereo aereo = new MezzoAereo();
            aereo.idMezzo = id;
            aereo.codiceMezzo = codice;
            aereo.postiPassegeri = postiPassegeri;
            aereo.controllore = controllore;
            aereo.fermate = fermate;
            mezziAereo.Add(aereo);
        }

        public void SalvaBiglietto(Biglietto biglietto)
        {
            bigliettiAerei.Add(biglietto);
        }
        public void SalvaViaggio(Viaggio viaggio)
        {
            viaggiAerei.Add(viaggio);
        }

        public void CheckIn(string  codiceBiglietto, Passegero passegero)
        {
            //CERCARE IN LIST Biglietti NUMERO BIGLIETTO E CONFRONTARLO CON BIGLIETTO PASSSATO

            var querry = from Biglietto in bigliettiAerei where Biglietto.codiceBiglietto == codiceBiglietto select Biglietto;
            foreach (Biglietto b in querry)
            {
                if (querry != null)
                {
                    Console.WriteLine("Codice biglietto trovato nella banca dati. CHECK IN AVENUTO CON SUCESSO!");
                }
                else
                {
                    Console.WriteLine("Codice non trovato non puoi fare check in.");
                }
            }
        }
        public override void AsociaControlloreAMezzo(Controllore controllore, Mezzo mezzo)
        { }//quando si crea agenzia chimo questo mettodo e asocio cont a mezzo
    }

    public interface ICheckIn
    {
        public void CheckIn(string codiceBiglietto,Passegero passegero);
    }

    public enum Fermate
    { 
        MI,NA,TO,VE,NO,BO,CZ,CA,BZ,FI,AR,RM
    }
    public abstract class Mezzo 
    {
        public int idMezzo;
        public string codiceMezzo;
        public Dictionary<int, Passegero> postiPassegeri; 
        public Passegero[] posti;
        public List<string> tratta; // puo contenere tutte le fermate o cita oppure fare un enumeratore
        public Controllore controllore;
        public List<Fermate> fermate; //questo e tratta, ogni volta quando creo mezzo aggiungo anche tratta di quello mezzo

        //public Mezzo(List<Fermate> fermate)
        //{
        //    this.fermate = fermate;
        //}
    }
    public class MezzoTerrestre : Mezzo
    {
        //public MezzoTerrestre(List<Fermate> fermate): base(fermate)
        //{ }
        public void AssegnaMezzo() { }

    }
    public class MezzoMaritimo : Mezzo
    {
        //public MezzoMaritimo(List<Fermate> fermate) : base(fermate)
        //{ }
    } 
    public class MezzoAereo : Mezzo
    {
        //public MezzoAereo(List<Fermate> fermate) : base(fermate)
        //{ }
    }

    public class Biglietteria
    {
        int i = 0;
        public List<Viaggio> viaggi = new List<Viaggio>(); 
        public List<AgenziaNazionale> agenzie = new List<AgenziaNazionale>();   
        public List<Biglietto> tuttiTipiDiBiglietti = new List<Biglietto>();   

        public void CreaViaggio(int id, string traggito, decimal prezzo, DateTime data, string agenziaNazionale,Mezzo mezzo)//aggiungere come parametro biglietto che ho creato sotto, anche controllore, e mezzo cosi asociamo tutto a viaggio prendere sempre controllori liberi
        {
             
            Viaggio viaggio = new Viaggio();
            viaggio.idViaggio = id;
            viaggio.traggito = traggito;
            viaggio.prezzo = prezzo;
            viaggio.data = data;//aggingere orario di partenzza e orario di arrivo
            viaggio.agenziaNazionaleNome = agenziaNazionale;
            viaggio.mezzo = mezzo;
            viaggi.Add(viaggio);
            
        }
        public void  RicercaViaggio(string tragitto)
        {
            //foreach (Viaggio v in viaggi)
            //{
            //   var result = v.traggito == tragitto;

            //    if (result)
            //    {
            //        Console.WriteLine("Il tragitto " + v.traggito + " è disponibile");
            //        break;
            //    }
            //    else if (!result) { Console.WriteLine("Il tragitto " + tragitto + " non  è disponibile"); break; }


            //}  

            var querry = from Viaggio in viaggi where Viaggio.traggito == tragitto  select Viaggio;
            foreach (Viaggio v in querry)
            {
                if (querry != null)
                {

                    Console.WriteLine("Viaggio " + v.traggito + " è disponibile per data " + v.data);
                }else 
                {

                    Console.WriteLine("Viaggio " + tragitto + "non è disponibile");


                }

            }
        }

        public void TuttiViaggiDisponibili()
        {
            Console.WriteLine("TUTTI VIAGGI DISPONIBILI:");
            foreach (Viaggio v in viaggi)
            {
                Console.WriteLine(" IdViaggio ->{0},Tragitto -> {1}, Prezzo -> {2}, Data -> {3}, Agenzia -> {4}",v.idViaggio,v.traggito, v.prezzo, v.data, v.agenziaNazionaleNome);
            }
        }

        public void AquistaOnline(Passegero passegero, Viaggio viaggio)
        {
            //pagare il biglietto online
            passegero.PagaBiglietto(passegero.cartaCredito, passegero.pin);
            
            Console.WriteLine("La tua carta di credito è stata debitata di " + viaggio.prezzo);
            
        }
        public void AquistaInBiglieteria(Passegero passegero, Viaggio viaggio)
        {
            passegero.PagaBiglietto(passegero.cartaCredito, passegero.pin);
           
            Console.WriteLine("La tua carta di credito è stata debitata di " + viaggio.prezzo);
        }

        //public void VisualizzaBiglietto(Viaggio viaggio, Biglietto biglietto, Passegero passegero)
        //{   
            
        //    //codice biglietto
        //    //agenzia
        //    //nome passegero
        //    //documento passegero
        //    //mezzo
        //    //posto

        //}
        public readonly Random rnd = new Random();
        public void CreaBiglietto(DateTime dataCreazione,Passegero passegero, Viaggio viaggio)
        {
            Biglietto biglietto = new Biglietto();
            biglietto.codiceBiglietto = rnd.Next(1000,5000).ToString();
            biglietto.dataCreazioneBiglietto = dataCreazione;
            biglietto.passegero = passegero;
            biglietto.viaggio = viaggio;
            i++;
            biglietto.idBiglietto = i;
            tuttiTipiDiBiglietti.Add(biglietto);

        }

        public void AsociaBigliettoAPassegero(Passegero passegero, Biglietto biglietto)
        { 

        }

        public void CercaTrattaPerData(string tratta)
        {
            var querry = from Viaggio in viaggi where Viaggio.traggito == tratta select Viaggio;
            foreach (Viaggio v in querry)
            {
                if (v != null)
                    Console.WriteLine("Viaggio " + v.traggito + " è disponibile per data " + v.data);
                else Console.WriteLine("Viaggio non disponibile");
            }

        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {


            //BIGLETERIA
            Biglietteria biglietteria = new Biglietteria();
            //creare oggetti agenzie
            AgenziaTerrestre agenziaTerrestre = new AgenziaTerrestre("ITerrestre");
            AgenziaMaritimo agenziaMarritimo = new AgenziaMaritimo("ITMarritimo");
            AgenziaAereo agenziaAereo = new AgenziaAereo("ITAereo");

            //aggiungere oggetti agenzie in lista agenzie
            biglietteria.agenzie.Add(agenziaTerrestre);
            biglietteria.agenzie.Add(agenziaMarritimo);
            biglietteria.agenzie.Add(agenziaAereo);

            //AGENZIA TERRESTRE creare controllori e mezzi
            agenziaTerrestre.CreaControllore(1, "Mario Rossi", "CT0001");
            agenziaTerrestre.CreaMezzo(1, "MT0001", new Dictionary<int, Passegero> {[1]= null,[2] = null, [3] = null,[4]=null, [5]= null, [6] = null, [7] = null, [8] = null, [9] = null, [10] = null }, agenziaTerrestre.controlloriTerrestre[0], new List<Fermate> {Fermate.MI,Fermate.BO,Fermate.FI,Fermate.RM });
            agenziaTerrestre.CreaControllore(2, "Alessio Ragni", "CT0002");
            agenziaTerrestre.CreaMezzo(2, "MT0002", new Dictionary<int, Passegero> { [1] = null, [2] = null, [3] = null, [4] = null, [5] = null, [6] = null, [7] = null, [8] = null, [9] = null }, agenziaTerrestre.controlloriTerrestre[1], new List<Fermate> { });

            //AGENZIA MARITTIMA creare controllori e mezzi
            agenziaMarritimo.CreaControllore(1, "Nicola Rossi", "CM0001");
            agenziaMarritimo.CreaMezzo(1, "MM0001", new Dictionary<int, Passegero> { [1] = null, [2] = null, [3] = null, [4] = null, [5] = null, [6] = null, [7] = null, [8] = null }, agenziaMarritimo.controlloriMarittimi[0], new List<Fermate> { });
            agenziaMarritimo.CreaControllore(2, "Stefano Ragni", "CM0002");
            agenziaMarritimo.CreaMezzo(2, "MM0002", new Dictionary<int, Passegero> { [1] = null, [2] = null, [3] = null, [4] = null, [5] = null, [6] = null, [7] = null }, agenziaMarritimo.controlloriMarittimi[1], new List<Fermate> { });

            //AGENZIA AEREO creare controllori e mezzi
            agenziaAereo.CreaControllore(1, "Anna Rossi", "CA0001");
            agenziaAereo.CreaMezzo(1, "MA0001", new Dictionary<int, Passegero> { [1] = null, [2] = null, [3] = null, [4] = null, [5] = null,[6]=null }, agenziaAereo.controlloriAereo[0], new List<Fermate> { });
            agenziaAereo.CreaControllore(2, "Sara Ragni", "CA0002");
            agenziaAereo.CreaMezzo(2, "MA0002", new Dictionary<int, Passegero> { [1] = null, [2] = null, [3] = null, [4] = null, [5] = null }, agenziaAereo.controlloriAereo[1], new List<Fermate> { });


            //creare dei viaggi con i propo dati
            biglietteria.CreaViaggio(1, "MILANO - ROMA", 50.5M, new DateTime(2022, 08, 04, 10, 20, 01), biglietteria.agenzie[0].nome, agenziaTerrestre.mezziTerrestre[0]);
            biglietteria.CreaViaggio(2, "GENOVA - TUNIS", (300M), new DateTime(2022, 09, 10, 12, 10, 02), biglietteria.agenzie[1].nome, agenziaMarritimo.mezziMarittimi[0]);
            biglietteria.CreaViaggio(3, "MILANO - NEWYORK", 500M, new DateTime(2022, 04, 25, 01, 30, 01), biglietteria.agenzie[2].nome, agenziaAereo.mezziAereo[0]);

            #region POSSIBILITA DI CERCARE VIAGGI E VEDERE TUTTI VIAGGI DISPONIBILI CON I PREZZI
            //utente sta faccendo la ricerca e fa vedere tutti i viaggi disponibili
            Console.WriteLine(" Benvenuti in biglietteria, ricerca il viaggio");

            Console.WriteLine(" Inserisci partenza");
            string partenza = Console.ReadLine();

            Console.WriteLine(" Inserisci destinazione ");
            string destinazione = Console.ReadLine();
            string ricercaViaggio = partenza + " - " + destinazione;
            

            //ricerca viaggo disponibile passando tragitto // dopo quando agiungo piu viaggi faccio anche per prezzo
            biglietteria.RicercaViaggio(ricercaViaggio.ToUpper());

            Console.WriteLine(" Se Vuoi vedere tutti i viaggi disponibili con i prezzi scrivi \"y\" se no scrivi \"n\"");
            string siNo = Console.ReadLine().ToLower();
            if (siNo == "y")
            {
                //tira fuori tutti i viaggi disponibili
                biglietteria.TuttiViaggiDisponibili();
            }
            else { Console.WriteLine(); }

            Console.WriteLine();
            #endregion 

            //chiedere ad passegero dove vuolle aquistare biglietto in biglietteria o online
            Console.WriteLine("AQUISTA BIGLIETTO PER DETERMINATO VIAGGIO");
          
            Console.WriteLine();
            Console.WriteLine("Inserisci tuo nome e cognome");
            string fullName =Console.ReadLine();
            //creare passegero
            Passegero passegero1 = new Passegero(1, fullName, "PA54261", 12345675, 1234);
            ProcediConAquisto(passegero1,biglietteria,agenziaAereo,agenziaMarritimo,agenziaTerrestre);
            Console.WriteLine("Inserisci tuo nome e cognome");
            string fulllName = Console.ReadLine();
            Passegero passegero2 = new Passegero(2, fulllName,"PA214353",1234567,3333);
            ProcediConAquisto(passegero2, biglietteria, agenziaAereo, agenziaMarritimo, agenziaTerrestre);
            Console.WriteLine("Compra un altro biglietto");
            ProcediConAquisto(passegero2, biglietteria, agenziaAereo, agenziaMarritimo, agenziaTerrestre);






        }
        public static void ProcediConAquisto(Passegero passegero, Biglietteria biglietteria,
            AgenziaAereo agenziaAereo, AgenziaMaritimo agenziaMaritimo,AgenziaTerrestre agenziaTerrestre)
        {
            Console.WriteLine("Buongiorno " + passegero.fullName + " questi sono tutti viaggi che si possono comprare in questa biglietteria");
            biglietteria.TuttiViaggiDisponibili();
            Console.WriteLine("");
            Console.WriteLine("Seleziona numero davanti tragitto per specificare qualle biglietto vuoi aquistare");
            #region prova try catch
            //try
            //{
            //    int viaggioSelezionato = int.Parse(Console.ReadLine()); //possibile avere System.FormatException: 'Input string was not in a correct format.'

            //    //distinguere quando viaggio è assiociato ad agenzia aereo e maritima perche in questi casi si fa aquisto online e se è agenzia terrestre si fa aquisto inBiglietreria
            //    var marittima = biglietteria.viaggi.Where(v => v.agenziaNazionaleNome == "ITMarritimo").FirstOrDefault(); //marittima (da corregere)
            //    var aereo = biglietteria.viaggi.Where(v => v.agenziaNazionaleNome == "ITAereo").FirstOrDefault();
            //    var terestre = biglietteria.viaggi.Where(v => v.agenziaNazionaleNome == "ITerrestre").FirstOrDefault();
            //    if (viaggioSelezionato == marittima.id || viaggioSelezionato == aereo.id)
            //    {
            //        Console.WriteLine("Aquista online");
            //    }
            //    else if (viaggioSelezionato == terestre.id)
            //    {
            //        Console.WriteLine("Aquista in biglietteria");
            //    }
            //}

            //catch (FormatException e)
            //{

            //    Console.WriteLine("Hai digitato carattere, devi digitare numero");
            //}
            #endregion
            int idInput = 0;
            bool validInput = false;
           
            while (!validInput)
            {

                Console.Write("\nDigita numero viaggio: ");
                string viaggioSelezzionato = Console.ReadLine();
                validInput = int.TryParse(viaggioSelezzionato, out idInput);

                if (!validInput)
                {
                    Console.WriteLine("\nFormato non valido! Hai digitato la lettera digita di nuovo.");
                    Console.Beep();
                }

                var marittima = biglietteria.viaggi.Where(v => v.agenziaNazionaleNome == "ITMarritimo").FirstOrDefault(); //marittima (da corregere)
                var aereo = biglietteria.viaggi.Where(v => v.agenziaNazionaleNome == "ITAereo").FirstOrDefault();
                var terestre = biglietteria.viaggi.Where(v => v.agenziaNazionaleNome == "ITerrestre").FirstOrDefault();

                //creare biglietto per determinato viaggio
                //creare biglietto in automatico incrementare id e renderizare codice
                biglietteria.CreaBiglietto( DateTime.Now, passegero, biglietteria.viaggi.Where(i => i.idViaggio == idInput).FirstOrDefault());
                var viaggio = biglietteria.viaggi.Where(x => x.idViaggio == idInput).FirstOrDefault();
                var posti = viaggio.mezzo.postiPassegeri;

                if (idInput == marittima.idViaggio)
                {
                    //controlare se ci sono posti dsponibili per questo viaggio
                    ///se ci sono posti disponibili fare seglierre a passeero posto e asegnare passegero a quello posto
                    foreach (KeyValuePair<int, Passegero> p in posti)
                    {
                        if (p.Value == null)
                        {
                            Console.WriteLine("Posto " + p.Key + " Disponibile");
                        }
                        else
                        {
                            Console.WriteLine("Posto " + p.Key + " Non disponibile");
                        }
                    }
                    Console.WriteLine("Segli posto digitando numero posto disponibile");
                    var postoSelezionato = Convert.ToInt32(Console.ReadLine());
                    
                    foreach (KeyValuePair<int, Passegero> p in posti)
                    {
                        posti[postoSelezionato] = passegero;
                    }
                    Console.WriteLine();
                    
                    foreach (KeyValuePair<int, Passegero> p in posti.Where(c=>c.Key== postoSelezionato))
                    {
                        Console.WriteLine("Passegero " + p.Value.fullName + " tuo codice posto è " + p.Key);
                    }
                    Console.WriteLine("Aquista online");
                    Console.WriteLine("Digita pin");
                    Console.WriteLine(passegero.fullName + " ricordati che il tuo pin è " + passegero.pin);
                    int pin = Convert.ToInt32(Console.ReadLine());
                    if (pin != passegero.pin)
                    {
                        Console.WriteLine("Pin sbagliato riprova");
                        while (pin != passegero.pin)
                        { pin = Convert.ToInt32(Console.ReadLine()); }
                    }
                    biglietteria.AquistaOnline(passegero, biglietteria.viaggi.Where(i => i.idViaggio == idInput).FirstOrDefault());

                    agenziaMaritimo.SalvaBiglietto(biglietteria.tuttiTipiDiBiglietti.Where(b => b.viaggio.idViaggio == idInput && b.passegero == passegero).FirstOrDefault());
                    agenziaMaritimo.SalvaViaggio(biglietteria.viaggi.Where(i => i.idViaggio == idInput).FirstOrDefault());
                    Console.WriteLine("--------------------------------------------------------------------------------------------");
                    Console.WriteLine("QUESTI SONO I DATI DI TUO BIGLIETTO SEI PRONTO PER FARE CHECK IN");
                    Console.WriteLine("BIGLIETTO DI:  " + passegero.fullName);
                    //faccio finta che per adesso un passegero puo comprare solo un biglietto
                    foreach (Biglietto b in biglietteria.tuttiTipiDiBiglietti.Where(x => x.passegero == passegero))
                    {
                        Console.WriteLine("CODICE BIGLIETTO -> {0}, DATA AQUISTO -> {1}, PREZZO: {2}", b.codiceBiglietto, b.dataCreazioneBiglietto, b.viaggio.prezzo);
                        Console.WriteLine("TRAGITTO: {0}, AZIENDA: {1}", b.viaggio.traggito, b.viaggio.agenziaNazionaleNome);
                        
                        Console.WriteLine("MEZZO: {0}, CODICE POSTO -> {1}", b.viaggio.mezzo.codiceMezzo, postoSelezionato);
                        Console.WriteLine("DATA E ORA DI PARTENZA -> {0}", b.viaggio.data);
                        //Console.WriteLine("DATA E ORA DI ARRIVO -> {0}", b.viaggio.dataArrivo);
                    }
                    Console.WriteLine("--------------------------------------------------------------------------------------------");
                    
                    Console.WriteLine("STAI PER FARE CHECK IN");
                    Console.WriteLine("Per fare check in digita codice che vedi sul bigletto");
                    string codiceInput = Console.ReadLine();
                    agenziaMaritimo.CheckIn(codiceInput, passegero);

                    Console.WriteLine("Per eventuali controlli sul bordo sei pregato di mostrare biglietto a controllore");

                    Console.WriteLine("Controllo di bigletto in corso");
                    Console.WriteLine("Digita codice del tuo biglietto");
                    string cInputMarittimo = Console.ReadLine();

                    var controlloreMarittimo = marittima.mezzo.controllore.codiceControllore;
                    var biglettoMarittimo = biglietteria.tuttiTipiDiBiglietti.Where(b => b.viaggio.idViaggio == idInput && b.passegero == passegero).FirstOrDefault();
                    foreach (Controllore c in agenziaMaritimo.controlloriMarittimi.Where(x => x.codiceControllore == controlloreMarittimo))
                    {
                        c.ControllaBigletto(cInputMarittimo);
                        if (biglettoMarittimo.codiceBiglietto == cInputMarittimo) { Console.WriteLine("Controllo aprovato dal controllore " + c.fullName + " suo codicce identificativo è " + c.codiceControllore); }
                        else { Console.WriteLine("Controllo non aprovato"); }
                    }

                }
                else if (idInput == aereo.idViaggio)
                {
                    foreach (KeyValuePair<int, Passegero> p in posti)
                    {
                        if (p.Value == null)
                        {
                            Console.WriteLine("Posto " + p.Key + " Disponibile");
                        }
                        else
                        {
                            Console.WriteLine("Posto " + p.Key + " Non disponibile");
                        }
                    }
                    Console.WriteLine("Segli posto digitando numero posto disponibile");
                    var postoSelezionato = Convert.ToInt32(Console.ReadLine());

                    foreach (KeyValuePair<int, Passegero> p in posti)
                    {
                        posti[postoSelezionato] = passegero;
                    }
                    Console.WriteLine();

                    foreach (KeyValuePair<int, Passegero> p in posti.Where(c => c.Key == postoSelezionato))
                    {
                        Console.WriteLine("Passegero " + p.Value.fullName + " tuo codice posto è " + p.Key);
                    }


                    Console.WriteLine("Aquista online");
                    Console.WriteLine("Digita pin");
                    Console.WriteLine(passegero.fullName + " ricordati che il tuo pin è " + passegero.pin);
                    int pin = Convert.ToInt32(Console.ReadLine());
                    if (pin != passegero.pin)
                    {
                        Console.WriteLine("Pin sbagliato riprova");
                        while (pin != passegero.pin)
                        { pin = Convert.ToInt32(Console.ReadLine()); }
                    }
                    biglietteria.AquistaOnline(passegero, biglietteria.viaggi.Where(i => i.idViaggio == idInput).FirstOrDefault());
                    agenziaAereo.SalvaViaggio(biglietteria.viaggi.Where(i => i.idViaggio == idInput).FirstOrDefault());
                    //qusto non va bene perche se compro piu di uno biglietto del stesso viagio non mi salva diverso biglietto prende sempre primo
                    agenziaAereo.SalvaBiglietto(biglietteria.tuttiTipiDiBiglietti.Where(b => b.viaggio.idViaggio == idInput && b.passegero ==passegero).FirstOrDefault());
                    Console.WriteLine("--------------------------------------------------------------------------------------------");
                    Console.WriteLine("QUESTI SONO I DATI DI TUO BIGLIETTO SEI PRONTO PER FARE CHECK IN");
                    Console.WriteLine("BIGLIETTO DI:  " + passegero.fullName);
                    //faccio finta che per adesso un passegero puo comprare solo un biglietto
                    foreach (Biglietto b in biglietteria.tuttiTipiDiBiglietti.Where(x => x.passegero == passegero))
                    {
                        Console.WriteLine("CODICE BIGLIETTO -> {0}, DATA AQUISTO -> {1}, PREZZO: {2}", b.codiceBiglietto, b.dataCreazioneBiglietto, b.viaggio.prezzo);
                        Console.WriteLine("TRAGITTO: {0}, AZIENDA: {1}", b.viaggio.traggito, b.viaggio.agenziaNazionaleNome);
                        Console.WriteLine("MEZZO: {0}, POSTO ->{1}", b.viaggio.mezzo.codiceMezzo,postoSelezionato);
                        Console.WriteLine("DATA E ORA DI PARTENZA -> {0}", b.viaggio.data);
                    }
                    foreach (Biglietto b in biglietteria.tuttiTipiDiBiglietti )
                    {
                        Console.WriteLine(b.idBiglietto);
                    }
                    foreach (Biglietto b in agenziaAereo.bigliettiAerei )
                    {
                        Console.WriteLine(b.idBiglietto);
                    }
                    Console.WriteLine("--------------------------------------------------------------------------------------------");
                    Console.WriteLine("STAI PER FARE CHECK IN");
                    Console.WriteLine("Per fare check in digita codice che vedi sul bigletto");
                    string codiceInput = Console.ReadLine();
                    agenziaAereo.CheckIn(codiceInput, passegero);
                    Console.WriteLine("Per eventuali controlli sul bordo sei pregato di mostrare biglietto a controllore");

                    Console.WriteLine("Controllo di bigletto in corso");
                    Console.WriteLine("Digita codice del tuo biglietto");
                    string cInputAereo = Console.ReadLine();

                    var controlloreAereo = aereo.mezzo.controllore.codiceControllore;
                    var biglettoAereo = biglietteria.tuttiTipiDiBiglietti.Where(b => b.viaggio.idViaggio == idInput && b.passegero == passegero).FirstOrDefault();
                    foreach (Controllore c in agenziaAereo.controlloriAereo.Where(x => x.codiceControllore == controlloreAereo))
                    {
                        c.ControllaBigletto(cInputAereo);
                        if (biglettoAereo.codiceBiglietto == cInputAereo) { Console.WriteLine("Controllo aprovato dal controllore " + c.fullName + " suo codicce identificativo è " + c.codiceControllore); }
                        else { Console.WriteLine("Controllo non aprovato"); }
                    }
                }
                else if (idInput == terestre.idViaggio)
                {
                    foreach (KeyValuePair<int, Passegero> p in posti)
                    {
                        if (p.Value == null)
                        {
                            Console.WriteLine("Posto " + p.Key + " Disponibile");
                        }
                        else
                        {
                            Console.WriteLine("Posto " + p.Key + " Non disponibile");
                        }
                    }
                    Console.WriteLine("Segli posto digitando numero posto disponibile");
                    var postoSelezionato = Convert.ToInt32(Console.ReadLine());

                    foreach (KeyValuePair<int, Passegero> p in posti)
                    {
                        posti[postoSelezionato] = passegero;
                    }
                    Console.WriteLine();

                    foreach (KeyValuePair<int, Passegero> p in posti.Where(c => c.Key == postoSelezionato))
                    {
                        Console.WriteLine("Passegero " + p.Value.fullName + " tuo codice posto è " + p.Key);
                    }


                    Console.WriteLine("Aquista in biglietteria");
                    Console.WriteLine("Digita pin");
                    Console.WriteLine(passegero.fullName + " ricordati che il tuo pin è " + passegero.pin);
                    int pin = Convert.ToInt32(Console.ReadLine());
                    if (pin != passegero.pin)
                    {
                        Console.WriteLine("Pin sbagliato riprova");
                        while (pin != passegero.pin)
                        { pin = Convert.ToInt32(Console.ReadLine()); }
                    }
                    biglietteria.AquistaInBiglieteria(passegero, biglietteria.viaggi.Where(i => i.idViaggio == idInput).FirstOrDefault());
                    agenziaTerrestre.SalvaViaggio(biglietteria.viaggi.Where(i => i.idViaggio == idInput).FirstOrDefault());
                    agenziaTerrestre.SalvaBiglietto(biglietteria.tuttiTipiDiBiglietti.Where(b => b.viaggio.idViaggio == idInput && b.passegero == passegero).FirstOrDefault());
                    Console.WriteLine("--------------------------------------------------------------------------------------------");
                    Console.WriteLine("QUESTI SONO I DATI DI TUO BIGLIETTO SEI PRONTO PER FARE CHECK IN");
                    Console.WriteLine("BIGLIETTO DI:  " + passegero.fullName);
                    
                    foreach (Biglietto b in biglietteria.tuttiTipiDiBiglietti.Where(x => x.passegero == passegero))
                    {
                        Console.WriteLine("CODICE BIGLIETTO -> {0}, DATA AQUISTO -> {1}, PREZZO: {2}", b.codiceBiglietto, b.dataCreazioneBiglietto, b.viaggio.prezzo);
                        Console.WriteLine("TRAGITTO: {0}, AZIENDA: {1}", b.viaggio.traggito, b.viaggio.agenziaNazionaleNome);
                        Console.WriteLine("MEZZO: {0}, POSTO->{1}", b.viaggio.mezzo.codiceMezzo,postoSelezionato);
                        var mezzo = b.viaggio.mezzo.fermate;
                        Console.WriteLine("MEZZO EFFETUERA LE FERMATE DI: ");
                        foreach (Fermate f in mezzo)
                        {
                            
                            Console.WriteLine(f);

                        }
                        Console.WriteLine("DATA E ORA DI PARTENZA -> {0}", b.viaggio.data);
                    }
                    Console.WriteLine("--------------------------------------------------------------------------------------------");
                    Console.WriteLine("Per eventuali controlli sul bordo sei pregato di mostrare biglietto a controllore");
                    Console.WriteLine("Controllo di bigletto in corso");
                    Console.WriteLine("Digita codice del tuo biglietto");
                    string cInput = Console.ReadLine();

                    var controlloreTerrestre = terestre.mezzo.controllore.codiceControllore;
                    var bigletto = biglietteria.tuttiTipiDiBiglietti.Where(b => b.viaggio.idViaggio == idInput && b.passegero == passegero).FirstOrDefault();
                    foreach (Controllore c in agenziaTerrestre.controlloriTerrestre.Where(x => x.codiceControllore == controlloreTerrestre))
                    {
                        c.ControllaBigletto(cInput);
                        if (bigletto.codiceBiglietto == cInput) { Console.WriteLine("Controllo aprovato dal controllore " + c.fullName + " suo codicce identificativo è " + c.codiceControllore); }
                        else { Console.WriteLine("Controllo non aprovato"); }
                    }
                }



            }


        }


    }
   
}
