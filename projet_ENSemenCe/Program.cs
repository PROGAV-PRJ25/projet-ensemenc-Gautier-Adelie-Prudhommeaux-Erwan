using System;
using System.Threading;
using System.Threading.Tasks;


// variable globale
bool enCours = true;
bool heure = false;

void Main()
    {
        Jardin jardiland = new Jardin();
        jardiland.PlacerTerrain("Petit Prince", 1, jardiland.MatTerrain);
        Console.WriteLine(jardiland);
        // Action principale (interaction utilisateur)
        while (enCours)
        {
            if(jardiland.Meteo.NumeroCata > 0){
                simulationurgence(jardiland.Meteo.NumeroCata,jardiland);
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("Voulez-vous arrêter le jeu ? O/N");
                string fin = Console.ReadLine()!;
                 while (fin != "O" || fin != "N")
                {
                    Console.WriteLine("vous avez taper un caractère invalide.");
                    Console.WriteLine("Voulez-vous arrêter le jeu ? O/N");
                    fin = Console.ReadLine()!;
                }
                if (fin == "O")
                {
                    enCours = false;
                }
            }else{
                simulation7Jourclassique();
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("Voulez-vous arrêter le jeu ? O/N");
                string fin = Console.ReadLine()!;
                 while (fin != "O" || fin != "N")
                {
                    Console.WriteLine("vous avez taper un caractère invalide.");
                    Console.WriteLine("Voulez-vous arrêter le jeu ? O/N");
                    fin = Console.ReadLine()!;
                }
                if (fin == "O")
                {
                    enCours = false;
                }
            }
        }
        Console.WriteLine("Jeu terminé.");
    }

void simulation1Jourclassique(){
// tour de jeu 

//afficher
//proposerposibilité joueur / répéter jusqu'a stop ou action =0
//fin journée / animaux déplacement / cgt meteo / cgt saison / degat action surplante / aparition et disparition du terrain / re^passer toutes les valeurs besoin a false / action objet /
}

void simulation7Jourclassique(){
    // une journée avec actions

    // les 6 jours sans actions
for (int i = 0; i<6; i++){
    simulation1Jourclassique();
}
}

 void simulationurgence(int numCata, Jardin jardin){
    if(numCata == 4){

        Console.Clear();
        Console.WriteLine("-----------------------------------------------------------------------------\n           ATTENTION UNE CATASTROPHE EST ARRIVÉE TOUS LES MAGASINS           \n                 ON FERMER LEUR PORTES POUR 28 JOURS!!!!!!!!                 \n-----------------------------------------------------------------------------");
        int choix;
        do
        {Console.WriteLine("Voulez-vous acheter quelque chose au magasin avant cela ?");
        choix = jardin.ListeChoix(["Oui","Non"]);
        if (choix == 0){
            jardin.Magasin();
        }}while(choix == 0);
        jardin.CriseEco = 4;
        jardin.SaisonChange();
        jardin.MeteoChange();
        jardin.TourActuel++;
    }else if (numCata == 8){
        Console.Clear(); 
        Console.WriteLine("-----------------------------------------------------------------------------\n              ATTENTION UNE CATASTROPHE EST ARRIVÉE LE TEMPS EST              \n                        DÉRÉGLÉ, LA SAISON CHANGE!!!!!!!!                     \n-----------------------------------------------------------------------------");
        Random rand = new Random();
        int S;
        do{ S = rand.Next(0,3);
        }while(jardin.ListeSaison[S] == jardin.Saison);
        jardin.Saison = jardin.ListeSaison[S];
        jardin.MeteoChange();
        jardin.TourActuel++;
    }else{
        Thread thread = new Thread(SimulationEnFond);
        thread.Start();
        heure = true;
        _ = AfficherHeureAsync();
        while(1==1){
    }
    }

}


async Task AfficherHeureAsync()
    {
        while (heure)
        {
            Console.SetCursorPosition(0, 0); // Affiche l'heure tout en haut
            Console.WriteLine("Heure : " + DateTime.Now.ToString("HH:mm:ss"));
            await Task.Delay(1000); // Met à jour chaque seconde
        }
    }

    


 void SimulationEnFond()
    {
        while (enCours)
        {
            Console.WriteLine("[Simu] Une action automatique se produit...");
            Thread.Sleep(1000); // Attend 2 secondes
        }
    }