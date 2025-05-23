using System;
using System.Threading;
using System.Threading.Tasks;

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
                SimulationUrgence(jardiland.Meteo.NumeroCata,jardiland);
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
            }else{
                Console.Clear();
                Simulation7JourClassique(jardiland);
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

void Simulation1JourClassique(Jardin jardin){
    while( jardin.NombreAction != 0){
    Console.WriteLine(jardin);
    Console.WriteLine($"Que voulez vous faire ? il vous reste {jardin.NombreAction}");
    int choix = jardin.ListeChoix(["Entretenir mon jardin","Allez au magasin","Finir la journée"]);
    if(choix == 0){
        jardin.Action(jardin.MatChoix(21,jardin),jardin);
    }else if(choix == 1 ){
        jardin.Magasin();
    }else{
        jardin.NombreAction = 0;
    }
    }
}

void Simulation7JourClassique(Jardin jardin){
    Simulation1JourClassique(jardin);
for (int i = 0; i<7; i++){
    FinJournée(jardin);
}
}

void FinJournée( Jardin jardin){
    foreach(Animaux animal in jardin.ListAnimaux){
        animal.Deplacer();
    }
    foreach(Plantes plante in jardin.ListPlante){
        foreach(Maladies maladie in plante.Maladie){
            for (int i=0; i<3;i++){
            plante.EtatActuel[i] += maladie.Degats[i];
            }
        }
        if(plante.EtatActuel[0] <= 0 ){
            jardin.SupprimerPlante(plante.Position);
        }
        if(jardin.TourActuel - plante.JourPlanter > plante.Longevite*30){
            jardin.SupprimerPlante(plante.Position);
        }
        plante.Proteger = false;
        plante.Explosion = false;
        if(plante.Nom == "Etoile"){
            plante.Deplacer();
        }
    } 
    Random ani = new Random();
    int chance = ani.Next(0,100);
    int x = ani.Next(0,21);
    int y = ani.Next(0,21);
    if (chance < 15){
                if (jardin.MatAnimaux[x, y] == -1)
                {
                    int choix =ani.Next(0,4);
                    if (choix == 0){
                        Animaux serpent = new Serpent([x,y],jardin);
                        jardin.ListAnimaux.Add(serpent);
                        jardin.MatAnimaux[x,y] = serpent.Id;
                    }else if(choix == 1){
                        Animaux mouton= new Mouton([x,y],jardin);
                        jardin.ListAnimaux.Add(mouton);
                        jardin.MatAnimaux[x,y] = mouton.Id;
                    }else if(choix == 2){
                        Animaux oiseau = new Oiseau([x,y],jardin);
                        jardin.ListAnimaux.Add(oiseau);
                        jardin.MatAnimaux[x,y] = oiseau.Id;
                    }else{
                        Elephant elephant = new Elephant([x,y],jardin);
                        int dir = elephant.Direction;
                        if(dir == 0 && jardin.MatAnimaux[x-1,y] == -1 ){
                            jardin.ListAnimaux.Add(elephant);
                            jardin.MatAnimaux[x,y] = elephant.Id;
                            jardin.MatAnimaux[x-1,y] = elephant.Id;
                        }else if(dir == 1 && jardin.MatAnimaux[x+1,y] == -1 ){
                            jardin.ListAnimaux.Add(elephant);
                            jardin.MatAnimaux[x,y] = elephant.Id;
                            jardin.MatAnimaux[x+1,y] = elephant.Id;
                        }else if(dir == 2 && jardin.MatAnimaux[x,y-1] == -1 ){
                            jardin.ListAnimaux.Add(elephant);
                            jardin.MatAnimaux[x,y] = elephant.Id;
                            jardin.MatAnimaux[x,y-1] = elephant.Id;
                        }else if(dir == 3 && jardin.MatAnimaux[x,y+1] == -1 ){
                            jardin.ListAnimaux.Add(elephant);
                            jardin.MatAnimaux[x,y] = elephant.Id;
                            jardin.MatAnimaux[x,y+1] = elephant.Id;
                        }
                    }
                }
    }
    Random mh = new Random();
    int chances = mh.Next(0,100);
    int coXx = mh.Next(0,21);
    int coYy = mh.Next(0,21);
    if (chances < 40 ){
                if (jardin.MatPlante[coXx, coYy] == -1)
                {
                    int choix = mh.Next(0,2);
                    if (choix == 0){
                        Plantes baobab = new Baobab([coXx,coYy],jardin);
                        jardin.ListPlante.Add(baobab);
                        jardin.MatPlante[coXx,coYy] = baobab.Id;
                    }else{
                        Plantes champi = new Champignon([coXx,coYy],jardin);
                        jardin.ListPlante.Add(champi);
                        jardin.MatPlante[coXx,coYy] = champi.Id;
                    }
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

    jardin.SaisonChange();
    jardin.MeteoChange();
    jardin.TourActuel++;
    jardin.NombreAction =0;
    jardin.NombreAction += 3 + 2*jardin.ListPlante.Count;
}

 void SimulationUrgence(int numCata, Jardin jardin){
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
        FinJournée(jardin);
    }else if (numCata == 8){
        Console.Clear(); 
        Console.WriteLine("-----------------------------------------------------------------------------\n              ATTENTION UNE CATASTROPHE EST ARRIVÉE LE TEMPS EST              \n                        DÉRÉGLÉ, LA SAISON CHANGE!!!!!!!!                     \n-----------------------------------------------------------------------------");
        Random rand = new Random();
        int S;
        do{ S = rand.Next(0,3);
        }while(jardin.ListeSaison[S] == jardin.Saison);
        jardin.Saison = jardin.ListeSaison[S];
        FinJournée(jardin);
    }else{
        Console.Clear();
        Thread thread = new Thread(() => SimulationEnFond(jardin));
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
                    jardin.Action(jardin.MatChoix(21,jardin),jardin);
                }else if(choix == 1 ){
                    jardin.Magasin();
                }   
            }
        }
        thread.Abort();
        FinJournée(jardin);
    }
}


async Task AfficherHeureAsync()
    {
        while (true)
        {
            Console.SetCursorPosition(0, 0); // Affiche l'heure tout en haut
            Console.WriteLine("Heure : " + DateTime.Now.ToString("HH:mm:ss"));
            await Task.Delay(1000); // Met à jour chaque seconde
        }
    }




 void SimulationEnFond(Jardin jardin)
    {
        while (enCours)
        {
           foreach(Animaux animal in jardin.ListAnimaux){
        animal.Deplacer();
    }
    foreach(Plantes plante in jardin.ListPlante){
        foreach(Maladies maladie in plante.Maladie){
            for (int i=0; i<3;i++){
            plante.EtatActuel[i] += maladie.Degats[i];
            }
        }
        if(plante.EtatActuel[0] <= 0 ){
            jardin.SupprimerPlante(plante.Position);
        }
        if(jardin.TourActuel - plante.JourPlanter > plante.Longevite*30){
            jardin.SupprimerPlante(plante.Position);
        }
        plante.Proteger = false;
        plante.Explosion = false;
        if(plante.Nom == "Etoile"){
            plante.Deplacer();
        }
    } 
    Random ani = new Random();
    int chance = ani.Next(0,100);
    int x = ani.Next(0,21);
    int y = ani.Next(0,21);
    if (chance < 15){
                if (jardin.MatAnimaux[x, y] == -1)
                {
                    int choix =ani.Next(0,4);
                    if (choix == 0){
                        Animaux serpent = new Serpent([x,y],jardin);
                        jardin.ListAnimaux.Add(serpent);
                        jardin.MatAnimaux[x,y] = serpent.Id;
                    }else if(choix == 1){
                        Animaux mouton= new Mouton([x,y],jardin);
                        jardin.ListAnimaux.Add(mouton);
                        jardin.MatAnimaux[x,y] = mouton.Id;
                    }else if(choix == 2){
                        Animaux oiseau = new Oiseau([x,y],jardin);
                        jardin.ListAnimaux.Add(oiseau);
                        jardin.MatAnimaux[x,y] = oiseau.Id;
                    }else{
                        Elephant elephant = new Elephant([x,y],jardin);
                        int dir = elephant.Direction;
                        if(dir == 0 && jardin.MatAnimaux[x-1,y] == -1 ){
                            jardin.ListAnimaux.Add(elephant);
                            jardin.MatAnimaux[x,y] = elephant.Id;
                            jardin.MatAnimaux[x-1,y] = elephant.Id;
                        }else if(dir == 1 && jardin.MatAnimaux[x+1,y] == -1 ){
                            jardin.ListAnimaux.Add(elephant);
                            jardin.MatAnimaux[x,y] = elephant.Id;
                            jardin.MatAnimaux[x+1,y] = elephant.Id;
                        }else if(dir == 2 && jardin.MatAnimaux[x,y-1] == -1 ){
                            jardin.ListAnimaux.Add(elephant);
                            jardin.MatAnimaux[x,y] = elephant.Id;
                            jardin.MatAnimaux[x,y-1] = elephant.Id;
                        }else if(dir == 3 && jardin.MatAnimaux[x,y+1] == -1 ){
                            jardin.ListAnimaux.Add(elephant);
                            jardin.MatAnimaux[x,y] = elephant.Id;
                            jardin.MatAnimaux[x,y+1] = elephant.Id;
                        }
                    }
                }
    }
    Random mh = new Random();
    int chances = mh.Next(0,100);
    int coXx = mh.Next(0,21);
    int coYy = mh.Next(0,21);
    if (chances < 40 ){
                if (jardin.MatPlante[coXx, coYy] == -1)
                {
                    int choix = mh.Next(0,2);
                    if (choix == 0){
                        Plantes baobab = new Baobab([coXx,coYy],jardin);
                        jardin.ListPlante.Add(baobab);
                        jardin.MatPlante[coXx,coYy] = baobab.Id;
                    }else{
                        Plantes champi = new Champignon([coXx,coYy],jardin);
                        jardin.ListPlante.Add(champi);
                        jardin.MatPlante[coXx,coYy] = champi.Id;
                    }
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
        }
    }

Main();