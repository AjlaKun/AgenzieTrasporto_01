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
        public abstract void CreaMezzo(int id, string codice, int numeroPosti, Controllore controllore);
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
        
        public override void CreaMezzo(int id, string codice, int numeroPosti, Controllore controllore )
        {
            MezzoTerrestre treno = new MezzoTerrestre();
            treno.idMezzo = id;
            treno.codiceMezzo = codice;
            treno.numeroPosti = numeroPosti;
            treno.controllore = controllore;
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
        public override void CreaMezzo(int id, string codice, int numeroPosti, Controllore controllore)
        {
            MezzoMaritimo nave = new MezzoMaritimo();
            nave.idMezzo = id;
            nave.codiceMezzo = codice;
            nave.numeroPosti = numeroPosti;
            nave.controllore = controllore;
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
            foreach (Biglietto b in bigliettiMarittimi)
            {
                if (b.codiceBiglietto == codiceBiglietto)
                {
                    Console.WriteLine("Codice biglietto trovato nella banca dati. CHECK IN AVENUTO CON SUCESSO!");
                }
                else { Console.WriteLine("Codice non trovato non puoi fare check in."); }
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
        public override void CreaMezzo(int id, string codice, int numeroPosti, Controllore controllore)
        {
            MezzoAereo aereo = new MezzoAereo();
            aereo.idMezzo = id;
            aereo.codiceMezzo = codice;
            aereo.numeroPosti = numeroPosti;
            aereo.controllore = controllore;
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
            foreach (Biglietto b in bigliettiAerei)
            {
                if (b.codiceBiglietto == codiceBiglietto)
                {
                    Console.WriteLine("Codice biglietto trovato nella banca dati. CHECK IN AVENUTO CON SUCESSO!");
                }
                else { Console.WriteLine("Codice non trovato non puoi fare check in."); }
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
        MI,NA,TO,VE,NO,BO,CZ,CA,BZ
    }
    public abstract class Mezzo 
    {
        public int idMezzo;
        public string codiceMezzo;
        public int numeroPosti;
        public List<string> Posti;
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
        {   // qualcosa non va bene qua (if non va bene provo fare con switch)
            foreach (Viaggio v in viaggi)
            {
               var result = v.traggito == tragitto;

                if (result)
                {
                    Console.WriteLine("Il tragitto " + v.traggito + " è disponibile");
                    break;
                }
                else if (!result) { Console.WriteLine("Il tragitto " + tragitto + " non  è disponibile"); break; }
                
                
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

        public void CreaBiglietto(int idBigletto, string codiceBiglietto,DateTime dataCreazione,Passegero passegero, Viaggio viaggio)
        {
            Biglietto biglietto = new Biglietto();
            biglietto.idBiglietto = idBigletto;
            biglietto.codiceBiglietto = codiceBiglietto;
            biglietto.dataCreazioneBiglietto = dataCreazione;
            biglietto.passegero = passegero;
            biglietto.viaggio = viaggio;
            tuttiTipiDiBiglietti.Add(biglietto);


        }

        public void AsociaBigliettoAPassegero(Passegero passegero, Biglietto biglietto)
        { 

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
            agenziaTerrestre.CreaControllore(1,"Mario Rossi","CT0001");
            agenziaTerrestre.CreaMezzo(1, "MT0001", 20, agenziaTerrestre.controlloriTerrestre[0]);
            agenziaTerrestre.CreaControllore(2,"Alessio Ragni","CT0002");
            agenziaTerrestre.CreaMezzo(2, "MT0002", 30, agenziaTerrestre.controlloriTerrestre[1]);

            //AGENZIA MARITTIMA creare controllori e mezzi
            agenziaMarritimo.CreaControllore(1,"Nicola Rossi","CM0001");
            agenziaMarritimo.CreaMezzo(1, "MM0001", 200, agenziaMarritimo.controlloriMarittimi[0]);
            agenziaMarritimo.CreaControllore(2,"Stefano Ragni","CM0002");
            agenziaMarritimo.CreaMezzo(2, "MM0002", 200, agenziaMarritimo.controlloriMarittimi[1]);

            //AGENZIA AEREO creare controllori e mezzi
            agenziaAereo.CreaControllore(1,"Anna Rossi","CA0001");
            agenziaAereo.CreaMezzo(1, "MA0001", 100, agenziaTerrestre.controlloriTerrestre[0]);
            agenziaAereo.CreaControllore(2,"Sara Ragni","CA0002");
            agenziaAereo.CreaMezzo(2, "MA0002", 100, agenziaTerrestre.controlloriTerrestre[1]);


            //creare dei viaggi con i propo dati
            biglietteria.CreaViaggio(1,"MILANO - ROMA", 50.5M, new  DateTime (2022,08,04, 10,20,01), biglietteria.agenzie[0].nome,agenziaTerrestre.mezziTerrestre[0]); 
            biglietteria.CreaViaggio(2,"GENOVA - TUNIS", (300M), new DateTime(2022,09,10, 12,10,02), biglietteria.agenzie[1].nome, agenziaMarritimo.mezziMarittimi[0]);
            biglietteria.CreaViaggio(3,"MILANO - NEWYORK", 500M, new DateTime(2022,04,25,01,30,01), biglietteria.agenzie[2].nome, agenziaAereo.mezziAereo[0]);
            //cercare il viaggio per data 

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

            Console.WriteLine("Buongiorno " + passegero1.fullName + " questi sono tutti viaggi che si possono comprare in questa biglietteria");
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
            //vedere come creare piu passegeri di non ripetere sempre tutto ciclo while
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
                biglietteria.CreaBiglietto(1, "B1234", DateTime.Now, passegero1, biglietteria.viaggi.Where(i => i.idViaggio == idInput).FirstOrDefault());
                

                if (idInput == marittima.idViaggio)
                {
                    Console.WriteLine("Aquista online");
                    Console.WriteLine("Digita pin");
                    Console.WriteLine(passegero1.fullName + " ricordati che il tuo pin è " + passegero1.pin);
                    int pin = Convert.ToInt32(Console.ReadLine());
                    if (pin != passegero1.pin)
                    {
                        Console.WriteLine("Pin sbagliato riprova");
                        while (pin != passegero1.pin)
                        { pin = Convert.ToInt32(Console.ReadLine()); }
                    }
                    biglietteria.AquistaOnline(passegero1, biglietteria.viaggi.Where(i => i.idViaggio == idInput).FirstOrDefault());

                    agenziaMarritimo.SalvaBiglietto(biglietteria.tuttiTipiDiBiglietti.Where(b=>b.viaggio.idViaggio == idInput).FirstOrDefault());
                    agenziaMarritimo.SalvaViaggio(biglietteria.viaggi.Where(i => i.idViaggio == idInput).FirstOrDefault());
                    Console.WriteLine("--------------------------------------------------------------------------------------------");
                    Console.WriteLine("QUESTI SONO I DATI DI TUO BIGLIETTO SEI PRONTO PER FARE CHECK IN");
                    Console.WriteLine("BIGLIETTO DI:  " + passegero1.fullName);
                    //faccio finta che per adesso un passegero puo comprare solo un biglietto
                    foreach (Biglietto b in biglietteria.tuttiTipiDiBiglietti.Where(x => x.passegero == passegero1))
                    {
                        Console.WriteLine("CODICE BIGLIETTO -> {0}, DATA AQUISTO -> {1}, PREZZO: {2}", b.codiceBiglietto, b.dataCreazioneBiglietto, b.viaggio.prezzo);
                        Console.WriteLine("TRAGITTO: {0}, AZIENDA: {1}", b.viaggio.traggito, b.viaggio.agenziaNazionaleNome);
                        //non ho assegnato posto a passegero
                        Console.WriteLine("MEZZO: {0}", b.viaggio.mezzo.codiceMezzo);
                        Console.WriteLine("DATA E ORA DI PARTENZA -> {0}", b.viaggio.data);
                    }
                    Console.WriteLine("--------------------------------------------------------------------------------------------");

                    Console.WriteLine("STAI PER FARE CHECK IN");
                    Console.WriteLine("Per fare check in digita codice che vedi sul bigletto");
                    string codiceInput =Console.ReadLine();
                    agenziaMarritimo.CheckIn(codiceInput,passegero1);

                    Console.WriteLine("Per eventuali controlli sul bordo sei pregato di mostrare biglietto a controllore");


                }
                else if (idInput == aereo.idViaggio) 
                {
                    Console.WriteLine("Aquista online");
                    Console.WriteLine("Digita pin");
                    Console.WriteLine(passegero1.fullName + " ricordati che il tuo pin è " + passegero1.pin);
                    int pin = Convert.ToInt32(Console.ReadLine());
                    if (pin != passegero1.pin)
                    {
                        Console.WriteLine("Pin sbagliato riprova");
                        while (pin != passegero1.pin)
                        { pin = Convert.ToInt32(Console.ReadLine()); }
                    }
                    biglietteria.AquistaOnline(passegero1, biglietteria.viaggi.Where(i => i.idViaggio == idInput).FirstOrDefault());
                    agenziaAereo.SalvaViaggio(biglietteria.viaggi.Where(i => i.idViaggio == idInput).FirstOrDefault());
                    agenziaAereo.SalvaBiglietto(biglietteria.tuttiTipiDiBiglietti.Where(b => b.viaggio.idViaggio == idInput).FirstOrDefault());
                    Console.WriteLine("--------------------------------------------------------------------------------------------");
                    Console.WriteLine("QUESTI SONO I DATI DI TUO BIGLIETTO SEI PRONTO PER FARE CHECK IN");
                    Console.WriteLine("BIGLIETTO DI:  " + passegero1.fullName);
                    //faccio finta che per adesso un passegero puo comprare solo un biglietto
                    foreach (Biglietto b in biglietteria.tuttiTipiDiBiglietti.Where(x => x.passegero == passegero1))
                    {
                        Console.WriteLine("CODICE BIGLIETTO -> {0}, DATA AQUISTO -> {1}, PREZZO: {2}", b.codiceBiglietto, b.dataCreazioneBiglietto, b.viaggio.prezzo);
                        Console.WriteLine("TRAGITTO: {0}, AZIENDA: {1}", b.viaggio.traggito, b.viaggio.agenziaNazionaleNome);
                        Console.WriteLine("MEZZO: {0}", b.viaggio.mezzo.codiceMezzo);
                        Console.WriteLine("DATA E ORA DI PARTENZA -> {0}", b.viaggio.data);
                    }
                    Console.WriteLine("--------------------------------------------------------------------------------------------");
                    Console.WriteLine("Per eventuali controlli sul bordo sei pregato di mostrare biglietto a controllore");

                }
                else if (idInput == terestre.idViaggio)
                {
                    Console.WriteLine("Aquista in biglietteria");
                    Console.WriteLine("Digita pin");
                    Console.WriteLine(passegero1.fullName + " ricordati che il tuo pin è " + passegero1.pin);
                    int pin = Convert.ToInt32(Console.ReadLine());
                    if (pin != passegero1.pin)
                    {
                        Console.WriteLine("Pin sbagliato riprova");
                        while (pin != passegero1.pin)
                        { pin = Convert.ToInt32(Console.ReadLine()); }
                    }
                    biglietteria.AquistaInBiglieteria(passegero1, biglietteria.viaggi.Where(i => i.idViaggio == idInput).FirstOrDefault());
                    agenziaTerrestre.SalvaViaggio(biglietteria.viaggi.Where(i => i.idViaggio == idInput).FirstOrDefault());
                    agenziaTerrestre.SalvaBiglietto(biglietteria.tuttiTipiDiBiglietti.Where(b => b.viaggio.idViaggio == idInput).FirstOrDefault());
                    Console.WriteLine("--------------------------------------------------------------------------------------------");
                    Console.WriteLine("QUESTI SONO I DATI DI TUO BIGLIETTO SEI PRONTO PER FARE CHECK IN");
                    Console.WriteLine("BIGLIETTO DI:  " + passegero1.fullName);
                    //faccio finta che per adesso un passegero puo comprare solo un biglietto
                    foreach (Biglietto b in biglietteria.tuttiTipiDiBiglietti.Where(x => x.passegero == passegero1))
                    {
                        Console.WriteLine("CODICE BIGLIETTO -> {0}, DATA AQUISTO -> {1}, PREZZO: {2}", b.codiceBiglietto, b.dataCreazioneBiglietto, b.viaggio.prezzo);
                        Console.WriteLine("TRAGITTO: {0}, AZIENDA: {1}", b.viaggio.traggito, b.viaggio.agenziaNazionaleNome);
                        Console.WriteLine("MEZZO: {0}", b.viaggio.mezzo.codiceMezzo);
                        Console.WriteLine("DATA E ORA DI PARTENZA -> {0}", b.viaggio.data);
                    }
                    Console.WriteLine("--------------------------------------------------------------------------------------------");
                    Console.WriteLine("Per eventuali controlli sul bordo sei pregato di mostrare biglietto a controllore");
                    Console.WriteLine("Controllo di bigletto in corso");
                    Console.WriteLine("Digita codice del tuo biglietto");
                    string cInput = Console.ReadLine();

                    var controllore = terestre.mezzo.controllore.codiceControllore;
                    var bigletto = biglietteria.tuttiTipiDiBiglietti.Where(b => b.viaggio.idViaggio == idInput).FirstOrDefault();
                    foreach (Controllore c in agenziaTerrestre.controlloriTerrestre.Where(x=>x.codiceControllore == controllore))
                    {
                        c.ControllaBigletto(cInput);
                        if (bigletto.codiceBiglietto == cInput) { Console.WriteLine("Controllo aprovato dal controllore "+ c.fullName + " suo codicce identificativo è "+ c.codiceControllore); }
                        else { Console.WriteLine("Controllo non aprovato"); }
                    }
                }
                

                



            }

            

            
           













        }
    }
   
}
