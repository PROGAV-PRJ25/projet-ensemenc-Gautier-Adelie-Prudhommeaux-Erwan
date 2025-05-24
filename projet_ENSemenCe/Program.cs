using System;
using System.Threading;
using System.Threading.Tasks;

// variable globale
bool enCours = true;
bool t = false;

void Main()
    {
        Jardin jardiland = new Jardin();
        jardiland.PlacerTerrain("Petit Prince", 1, jardiland.MatTerrain);
        Console.WriteLine(jardiland);
        Console.SetCursorPosition(0, 2);
        // Action principale 
        while (enCours)
        {
            Console.SetCursorPosition(0, 2);
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
    int choix = jardin.ListeChoix(["Entretenir mon jardin","Allez au magasin","Finir la journée"],jardin);
    Console.WriteLine(jardin);
    if(choix == 0){
        jardin.Action(jardin.MatChoix(21,jardin),jardin);
    }else if(choix == 1 ){
        jardin.Magasin(jardin);
    }else{
        jardin.NombreAction = 0;
    }
    }
}

void Simulation7JourClassique(Jardin jardin){
    Simulation1JourClassique(jardin);
    Console.WriteLine("Journée finit");
    Thread.Sleep(1000);
for (int i = 0; i<7; i++){
    if(jardin.Meteo.NumeroCata <= 0){
    FinJournée(jardin);
    Console.WriteLine(jardin);
    Console.WriteLine("Jour suivant");
    Thread.Sleep(1000);
    }else{
        Console.Clear();
        Console.WriteLine("OOOOH NOOON UNE CATASTROPHE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        Thread.Sleep(1000);
        break;
    }

}
}

void FinJournée( Jardin jardin){
    if(jardin.ListAnimaux.Count == 0){
            }else{
                List<Animaux> liste =  new List<Animaux> {};
                foreach(Animaux animal in jardin.ListAnimaux){
                liste.Add(animal);
                }
           foreach(Animaux animal in liste){
        animal.Deplacer();
    }}
    if(jardin.ListPlante.Count == 0){

            }else{
                List<Plantes> liste1 =  new List<Plantes> {};
                foreach(Plantes plante in jardin.ListPlante){
                liste1.Add(plante);
                }
    foreach(Plantes plante in liste1){
        if(plante.Maladie.Count == 0){

            }else{
                List<Maladies> liste2 =  new List<Maladies> {};
                foreach(Maladies maladie in plante.Maladie){
                liste2.Add(maladie);
                }
        foreach(Maladies maladie in liste2){
            for (int i=0; i<3;i++){
            plante.EtatActuel[i] += maladie.Degats[i];
            }
        }}
        if(plante.Nom == "Etoile"){
            plante.Deplacer();
        }
        if(plante.EtatActuel[0] <= 0 ){
            jardin.SupprimerPlante(plante.Position);
        }
        if(jardin.TourActuel - plante.JourPlanter > plante.Longevite*30){
            jardin.SupprimerPlante(plante.Position);
        }
        plante.Proteger = false;
        plante.Explosion = false;
    } }
    Random ani = new Random();
    int chance = ani.Next(0,100);
    int x = ani.Next(0,21);
    int y = ani.Next(0,21);
    if (chance < 40){
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
                    }else{if (y != 0 && y != 20 && x != 0 && x != 20){
                        Elephant elephant = new Elephant([x,y],jardin);
                        int dir = elephant.Direction;
                        if(dir == 0 && jardin.MatAnimaux[x+1,y] == -1 ){
                            jardin.ListAnimaux.Add(elephant);
                            jardin.MatAnimaux[x,y] = elephant.Id;
                            jardin.MatAnimaux[x+1,y] = elephant.Id;
                        }else if(dir == 1 && jardin.MatAnimaux[x-1,y] == -1 ){
                            jardin.ListAnimaux.Add(elephant);
                            jardin.MatAnimaux[x,y] = elephant.Id;
                            jardin.MatAnimaux[x-1,y] = elephant.Id;
                        }else if(dir == 2 && jardin.MatAnimaux[x,y+1] == -1 ){
                            jardin.ListAnimaux.Add(elephant);
                            jardin.MatAnimaux[x,y] = elephant.Id;
                            jardin.MatAnimaux[x,y+1] = elephant.Id;
                        }else if(dir == 3 && jardin.MatAnimaux[x,y-1] == -1 ){
                            jardin.ListAnimaux.Add(elephant);
                            jardin.MatAnimaux[x,y] = elephant.Id;
                            jardin.MatAnimaux[x,y-1] = elephant.Id;
                        }
                    }}
                }
    }
    Random mh = new Random();
    int chances = mh.Next(0,100);
    int coXx = mh.Next(0,21);
    int coYy = mh.Next(0,21);
    if (chances < 60 ){
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
    jardin.NombreAction = 0;
    jardin.NombreAction += 3 + jardin.ListPlante.Count;
}

 void SimulationUrgence(int numCata, Jardin jardin){
    if(numCata == 4){

        Console.Clear();
        Console.WriteLine("-----------------------------------------------------------------------------\n           ATTENTION UNE CATASTROPHE EST ARRIVÉE TOUS LES MAGASINS           \n                 ON FERMER LEUR PORTES POUR 28 JOURS!!!!!!!!                 \n-----------------------------------------------------------------------------");
        string[] liste = ["Oui","Non"];
        int choix = 0;
        do
        {Console.WriteLine("Voulez-vous acheter quelque chose au magasin avant cela ?");
        int indexSelection = 0;
        int len = liste.Length;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------------------------------------------------\n           ATTENTION UNE CATASTROPHE EST ARRIVÉE TOUS LES MAGASINS           \n                 ON FERMER LEUR PORTES POUR 28 JOURS!!!!!!!!                 \n-----------------------------------------------------------------------------");
            Console.WriteLine("Sélectionnez avec les flèches, puis appuyez sur Entrée pour valider :\n");

            for (int i = 0; i < len; i++)
            {
                if (i == indexSelection)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"> {liste[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {liste[i]}");
                }
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow && indexSelection > 0)
                indexSelection--;
            else if (key == ConsoleKey.DownArrow && indexSelection < len - 1)
                indexSelection++;

        } while (key != ConsoleKey.Enter);
        choix = indexSelection;
        if (choix == 0){
            jardin.Magasin(jardin);
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
        Thread.Sleep(1000);
        FinJournée(jardin);
    }else{
        Console.Clear();
        t = true;
        Thread thread = new Thread(() => SimulationEnFond(jardin));
        thread.Start();
        DateTime debut = DateTime.Now;
        DateTime fin = debut.AddMinutes(2);
        _ = AfficherHeureAsync();
        jardin.NombreAction *= 10;
        while(DateTime.Now < fin){
            Console.SetCursorPosition(0, 2); // met la position sur la ranger du dessous pour ne pas suprimer l'heure
            if( jardin.NombreAction != 0){
                Console.WriteLine(jardin);
                Console.WriteLine("Que voulez vous faire ?");
                int choix = jardin.ListeChoix(["Entretenir mon jardin","Allez au magasin"],jardin);
                if(choix == 0){
                    jardin.Action(jardin.MatChoix(21,jardin),jardin);
                }else if(choix == 1 ){
                    jardin.Magasin(jardin);
                }  
            }else{
                while(DateTime.Now < fin){
                Console.WriteLine(jardin);
                Thread.Sleep(1000);
                }
            }
        }
        t =false;
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
        while (t)
        {
            Thread.Sleep(1000);
            if(jardin.ListAnimaux.Count == 0){

            }else{
                List<Animaux> liste =  new List<Animaux> {};
                foreach(Animaux animal in jardin.ListAnimaux){
                liste.Add(animal);
                }
           foreach(Animaux animal in liste){
        animal.Deplacer();
    }}
    if(jardin.ListPlante.Count == 0){

            }else{
                List<Plantes> liste1 =  new List<Plantes> {};
                foreach(Plantes plante in jardin.ListPlante){
                liste1.Add(plante);
                }
    foreach(Plantes plante in liste1){
        if(plante.Maladie.Count == 0){

            }else{
                List<Maladies> liste2 =  new List<Maladies> {};
                foreach(Maladies maladie in plante.Maladie){
                liste2.Add(maladie);
                }
        foreach(Maladies maladie in liste2){
            for (int i=0; i<3;i++){
            plante.EtatActuel[i] += maladie.Degats[i];
            }
        }}
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
        }
    Random ani = new Random();
    int chance = ani.Next(0,100);
    int x = ani.Next(0,21);
    int y = ani.Next(0,21);
    if (chance < 50){
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
                        if (y != 0 && y != 20 && x != 0 && x != 20){
                        if(dir == 0 && jardin.MatAnimaux[x+1,y] == -1 ){
                            jardin.ListAnimaux.Add(elephant);
                            jardin.MatAnimaux[x,y] = elephant.Id;
                            jardin.MatAnimaux[x+1,y] = elephant.Id;
                        }else if(dir == 1 && jardin.MatAnimaux[x-1,y] == -1 ){
                            jardin.ListAnimaux.Add(elephant);
                            jardin.MatAnimaux[x,y] = elephant.Id;
                            jardin.MatAnimaux[x-1,y] = elephant.Id;
                        }else if(dir == 2 && jardin.MatAnimaux[x,y+1] == -1 ){
                            jardin.ListAnimaux.Add(elephant);
                            jardin.MatAnimaux[x,y] = elephant.Id;
                            jardin.MatAnimaux[x,y+1] = elephant.Id;
                        }else if(dir == 3 && jardin.MatAnimaux[x,y-1] == -1 ){
                            jardin.ListAnimaux.Add(elephant);
                            jardin.MatAnimaux[x,y] = elephant.Id;
                            jardin.MatAnimaux[x,y-1] = elephant.Id;
                        }
                        }
                    }
                }
    }
    Random mh = new Random();
    int chances = mh.Next(0,100);
    int coXx = mh.Next(0,21);
    int coYy = mh.Next(0,21);
    if (chances < 50 ){
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