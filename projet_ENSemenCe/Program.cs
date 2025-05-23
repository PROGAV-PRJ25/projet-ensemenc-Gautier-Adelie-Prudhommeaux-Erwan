using System;
using System.Threading;
using System.Threading.Tasks;

// a regarder : list prix achat et vente / llist besoin plante / remplir liste plante par terrain / acheter terrain ajout de tout / taux d'apparition pour les animaux

// variable globale
bool enCours = true;

void Main()
    {
        Jardin jardiland = new Jardin();
        jardiland.PlacerTerrain("Petit Prince", 1, jardiland.MatTerrain);
        Console.WriteLine(jardiland);
        // Action principale 
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
                Console.Clear();
                simulation7Jourclassique();
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("Voulez-vous arrêter le jeu ? O/N");
                string fin = Console.ReadLine()!;
                 while (fin != "O" && fin != "N")
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

void simulation1Jourclassique(Jardin jardin){
    while( jardin.NombreAction != 0){
    Console.WriteLine(jardin);
    Console.WriteLine("Que voulez vous faire ?");
    int choix = jardin.ListeChoix(["Entretenir mon jardin","Allez au magasin","Finir la journée"]);
    if(choix == 0){
        action(MatChoix(21,jardin));
    }else if(choix == 1 ){
        jardin.magasin();
    }
    }
}

void simulation7Jourclassique(){
    simulation1Jourclassique();
for (int i = 0; i<7; i++){
    FinJournée();
}
}

void FinJournée( jardin jardin){
    foreach(Animaux animal in jardin.ListAnimaux){
        animal.Deplacer();
    }
    foreach(Plantes plante in jardin.ListPlante){
        foreach(Maladies maladie in plante.Maladie){
            plante.EtatActuel + maladie.dégats;
        }
        if(plante.EtatActuel[0] <= 0 ){
            SupprimerPlante(plante.Position);
        }
    }
    for (int i =0 ; i<21;i++){
        for(int j =0; j<21;j++){
            if (jardin.MatObjets[i,j] > 2){
                jardin.Applaudir([i,j]);
                jardin.MatObjets[i,j]--;
            }
        }
    }

    SaisonChange();
    MeteoChange();
    TourActuel++; //degat action surplante / aparition et disparition du terrain / repasser toutes les valeurs besoin a false / aparition animaux
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
        FinJournée();
    }else if (numCata == 8){
        Console.Clear(); 
        Console.WriteLine("-----------------------------------------------------------------------------\n              ATTENTION UNE CATASTROPHE EST ARRIVÉE LE TEMPS EST              \n                        DÉRÉGLÉ, LA SAISON CHANGE!!!!!!!!                     \n-----------------------------------------------------------------------------");
        Random rand = new Random();
        int S;
        do{ S = rand.Next(0,3);
        }while(jardin.ListeSaison[S] == jardin.Saison);
        jardin.Saison = jardin.ListeSaison[S];
        FinJournée();
    }else{
        Console.clear();
        Thread thread = new Thread(SimulationEnFond);
        thread.Start();
        DateTime debut = DateTime.Now;
        DateTime fin = debut.AddMinutes(5);
        _ = AfficherHeureAsync();
        while(DateTime.Now < fin){
            Console.SetCursorPosition(0, 2); // met la position sur la ranger du dessous pour ne pas suprimer l'heure
            if( jardin.NombreAction != 0){
                Console.WriteLine(jardin);
                Console.WriteLine("Que voulez vous faire ?");
                int choix = jardin.ListeChoix(["Entretenir mon jardin","Allez au magasin","Finir la journée"]);
                if(choix == 0){
                    action(MatChoix(21,jardin));
                }else if(choix == 1 ){
                    jardin.magasin();
                }   
            }
        }
        thread.Stop();
        FinJournée();
    
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
            
        }
    }

Main();