using System;

public class Jardin
{
    public int TourActuel { get; set; }   //Le nombre de jours depuis le début de la simulation
    public int PoudreEtoile { get; set; }  //La quantité de poudre d'étoile (= argent) du joueur
    public List<Saisons> ListeSaison { get; set; }
    public Saisons Saison { get; set; }
    public Meteos Meteo { get; set; }
    public int[,] MatPlante { get; set; }   //La représentation du plateau de jeu, dans laquelle sont localisées toutes les plantes. Chaque plante est représentée par son indice
    public int[,] MatAnimaux { get; set; }      //La représentation du plateau de jeu, dans laquelle sont localisés tous les animaux. Chaque animal est représenté par son indice
    public int[,] MatObjets { get; set; }     //La représentation du plateau de jeu, dans laquelle sont localisés tous les pbjets posés par le joueur
    public string[] ActionPossible { get; set; }  //La liste des actions possibles par le joueur
    public int NombreAction { get; set; }
    public int[] Objects { get; set; }   // chaque indice correspond a un object. il y en a 25 
    public string[] PlantesJouable { get; set; }   //La liste des plantes accessibles (de nouvelles plantes se rajoutent quand on achète une autre planète)
    public string[] MaladiesPossible { get; set; }
    public string[] ObjectsAchetable { get; set; }
    public int[] GrainesDisponibles { get; set; }  //chaque indice est associé a une plante. il y en a 11 
    public List<Plantes> ListPlante { get; set; }  //La liste des plantes présentes sur le plateau (sur MatPlante)
    public List<Animaux> ListAnimaux { get; set; }  //La liste des animaux présents sur le plateau
    public int[,] MatTerrain { get; set; }
    public int CriseEco {get;set;}


    public Jardin()
    {
        TourActuel = 0;
        CriseEco = 0;
        NombreAction = 3;
        PoudreEtoile = 100;
        Meteo = new Calme();
        ListeSaison = new List<Saisons> { new Saison1(), new Saison2(), new Saison3() };
        Saison = ListeSaison[0];
        PlantesJouable = ["Etoile", "Météorite", "Rose", "Chapeau", "Nuage"];
        ActionPossible = ["Objet", "Planter", "Récolter", "Déraciné", "Arroser", "Protéger", "Effrayer", "Applaudir","Regarder"];
        MaladiesPossible = ["Maladie1"];
        ObjectsAchetable = ["Lanterne", "Pelle", "Écharpe", "Paravent", "Arrosoir", "Haut parleur", "Médicament", "Pommade"]; //cloture et epouventails
        Objects = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
        //[Lanterne, Pelle, Echarpe, Paravent, Arrosoir, Haut parleur, Medicament, Pommade, Etoile, Météorite, Rose, Chapeau, Nuage, Etoile filante, Alcool, Soleil, Couronne, Planète, poussière d'étoile]
        GrainesDisponibles = [10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
        // [Etoile, Météorite, Rose, Chapeau, Nuage, Etoile filante, Alcootier, Plante orgueilleuse, Couronne, Planète, Lampadaire]
        MatPlante = new int[21, 21];
        MatAnimaux = new int[21, 21];
        MatObjets = new int[21, 21];
        MatTerrain = new int[3, 3]{
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 }
        };
        ListAnimaux = new List<Animaux> { };
        ListPlante = new List<Plantes> { };
    }

// fonction qui change la meteo 
    public void MeteoChange(){
        double somme = 0;
        double[] pourcent = Saison.PourcentMeteos; // change la meteo en fonction des pourcentage evolutif avec les terrains acheté
        Random rand = new Random();
        int indMeteo = rand.Next(1,1001);
        for(int i =0;i<13;i++){
            if (pourcent[i]!=0){
                somme += pourcent[i]*10;
                if (indMeteo<somme){
                    List<Meteos> liste = [new Calme(), new Pluie(),new Nuit(), new Soleil(), new Secheresse(), new Gel(), new TempeteStellaire(), new CriseEconomique(), new FortePluie(), new HordeAnimaux(), new Obligation(), new ChangementSaison(), new ToutNoir() ];
                    Meteo = liste[i];
                    break;
                }
            }
        }
    }

// fonction qui change la saison 
    public void SaisonChange(){
        if(TourActuel/30 >(TourActuel-1)/30){ // change si 30 jour passé
             Saison = ListeSaison[(TourActuel/30)%3];
        }
}

    public int[] MatChoix(int size, Jardin jardin){
        // permet de faire un choix sur une matrice avec les flêches
        int cursorX = 0;
        int cursorY = 0;
        bool found = false;

        for (int y = 0; y < size && !found; y++)
        {
            for (int x = 0; x < size && !found; x++)
            {
                if (jardin.MatPlante[x, y] != 0)
                {
                    cursorX = x;
                    cursorY = y;
                    found = true;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine("vous n'avez pas de terrain ou faire une action");
        }
        else
        {
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine(jardin);
                Console.WriteLine($"Sélectionnez une case libre avec les flèches pour sélectionnés l'endroit ou vous voulez faire votre action puis appuyez sur Entrée :\n");

                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        bool estSelection = (x == cursorX && y == cursorY);
                        bool estLibre = jardin.MatPlante[x, y] != 0;

                        if (estSelection)
                        {
                            if (estLibre)
                            {
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        else if (!estLibre)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        }
                        if(jardin.MatPlante[x,y] ==-1){
                            Console.Write(" .");
                        }else if (jardin.MatPlante[x,y] == 0){
                            Console.Write(" X");
                        }else{
                            Console.Write(" o");
                        }
                        Console.ResetColor();
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }

                key = Console.ReadKey(true).Key;

                int newX = cursorX;
                int newY = cursorY;

                switch (key)
                {
                    case ConsoleKey.LeftArrow: newX = Math.Max(0, cursorX - 1); break;
                    case ConsoleKey.RightArrow: newX = Math.Min(size - 1, cursorX + 1); break;
                    case ConsoleKey.UpArrow: newY = Math.Max(0, cursorY - 1); break;
                    case ConsoleKey.DownArrow: newY = Math.Min(size - 1, cursorY + 1); break;
                }

                // Se déplacer uniquement si la case est libre
                if (jardin.MatPlante[newX, newY] != 0)
                {
                    cursorX = newX;
                    cursorY = newY;
                }

            } while (key != ConsoleKey.Enter);
        }
        return [cursorX,cursorY];
    }
    public int ListeChoix(string[] liste,Jardin jardin)
    {
        // permet d'avoir une liste de choix avec les flêches
        int indexSelection = 0;
        int len = liste.Length;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine(jardin);
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
        return indexSelection;
    }


    public void Magasin(Jardin jardin)
    {
        // permet d'acheter ou de vendre soit des objets / terrains / plante
        if (CriseEco > 0){// si catastrophe plus de magasin
            Console.WriteLine("Vous ne pouvez pas acheter, c'est la crisis total here !!!!!!");
            Thread.Sleep(1000);
        }else{
        Console.WriteLine("Que voulez vous faire ? ");
        string[] l1 = ["Acheter", "Vendre","sortir"];
        int index1 = ListeChoix(l1,jardin);
        if (index1 == 0)
        {
            Console.WriteLine("Dans quelle catégorie voulez vous acheter ? ");
            string[] l2 = ["Objets", "Terrains","Plantes", "sortir"];
            int index2 = ListeChoix(l2, jardin);
            if (index2 == 0)
            {
                // achat objets
                Console.WriteLine("Que voulez vous acheter ? ");
                int index3 = ListeChoix(ObjectsAchetable,jardin);
                // vérification si il peux l'acheter avec son argent 
                AcheterObjet(index3);
                NombreAction--;
                PoudreEtoile-= 20;
            }
            else if (index2 == 2)
            {
                // achat plante
                Console.WriteLine("Que voulez vous acheter ? tout coûte 20 poussières d'étoiles");
                Thread.Sleep(1000);
                int index3 = ListeChoix(PlantesJouable,jardin);
                if(PoudreEtoile > 20){
                // vérification si il peux l'acheter avec son argent 
                if (index3 < 5){
                GrainesDisponibles[index3]++;
                }else{
                     if (PlantesJouable[index3] == "Etoile filante")
            {
                GrainesDisponibles[5]++;
            }
            else if (PlantesJouable[index3] == "Alcootier" )
            {
                GrainesDisponibles[6]++;
            }
            else if (PlantesJouable[index3] == "Plante orgueilleuse" )
            {
                GrainesDisponibles[7]++;
            }
            else if (PlantesJouable[index3] == "Couronne" )
            {
                GrainesDisponibles[8]++;
            }
            else if (PlantesJouable[index3]  == "Planète" )
            {
                GrainesDisponibles[9]++;
            }
            else if (PlantesJouable[index3] == "Lampadaire" )
            {
                GrainesDisponibles[10]++;
            }
        else { Console.WriteLine("Cette plante n'est pas encore disponible"); }

                    }
                    NombreAction--;
                    PoudreEtoile-= 20;
                }else{
                    Console.WriteLine("Vous n'avez pas assez d'argent");
                    Thread.Sleep(1000);
                }
                
            }
            else if(index2 == 1)
            {
                // achat terrains
                Console.Clear();
                Console.WriteLine("Que voulez vous acheter ? tout coûte 200 poussières d'étoiles");
                Thread.Sleep(1000);
                AcheterTerrain(jardin);
            }
        } else if (index1 == 2) {
        }
        else
        {
            // vendre plante ou objet
            Console.WriteLine("Que voulez vous vendre ? tout se vend 30 poussières d'étoiles");
            string[] list = [$"Lanterne, Nombre possédé : {Objects[0]}", $"Pelle, Nombre possédé : {Objects[1]}", $"Echarpe, Nombre possédé : {Objects[2]}", $"Paravent, Nombre possédé : {Objects[3]}", $"Arrosoir, Nombre possédé : {Objects[4]}", $"Haut parleur, Nombre possédé : {Objects[5]}", $"Medicament, Nombre possédé : {Objects[6]}", $"Pommade, Nombre possédé : {Objects[7]}", $"Etoile, Nombre possédé : {Objects[8]}", $"Météorite, Nombre possédé : {Objects[9]}", $"Rose, Nombre possédé : {Objects[10]}", $"Chapeau, Nombre possédé : {Objects[11]}", $"Nuage, Nombre possédé : {Objects[12]}", $"Etoile filante, Nombre possédé : {Objects[13]}", $"Alcool, Nombre possédé : {Objects[14]}", $"Soleil, Nombre possédé : {Objects[15]}", $"Couronne, Nombre possédé : {Objects[16]}", $"Planète, Nombre possédé : {Objects[17]}", $"poussière d'étoile, Nombre possédé : {Objects[18]}", "sortir"];
            int id = ListeChoix(list,jardin);
            if(id != 19 ){
            if (Objects[id] == 0)
            {
                Console.WriteLine("Vous n'en n'avez pas, vous ne pouvez pas en vendre !");
                Console.WriteLine("Voulez-vous faire une autre utilisation du magasin ?");
                Thread.Sleep(2000);
                int  taper = ListeChoix(["Oui","Non"],jardin);
                if (taper == 0)
                {
                    Magasin(jardin);
                }

            }
            else
            {// nombre a vendre
                Console.WriteLine("Combien voulez vous en vendre");
                int nombre = int.Parse(Console.ReadLine()!);
                if (nombre > Objects[id])
                {
                    Console.WriteLine("Vous n'en avez pas assez pour en vendre autant");
                    Console.WriteLine("Voulez-vous faire une autre utilisation du magasin ?");
                    Thread.Sleep(2000);
                    int taper1 = jardin.ListeChoix(["Oui","Non"],jardin);
                    if (taper1 == 0)
                    {
                        Magasin(jardin);
                    }
                }
                else
                {
                    int prix = 30; // prix 
                    PoudreEtoile += prix * nombre;
                    Objects[id] -= nombre;
                    NombreAction--;
                }
            }
        }
        }
        }
    }

    public void Action(int[] coord, Jardin jardin)
    {
        // demande l'action par rapport a toute les actions possible
        int index = ListeChoix(ActionPossible,jardin); 
        if(index == 1){
            string[] liste = PlantesJouable;
            int indexSelection = 0;
        int len = liste.Length;
        ConsoleKey key;

        do
        {
            // selection de la plante a planter
            Console.Clear();
            Console.WriteLine(jardin);
            Console.WriteLine("Quelle plante voulez-vous planter ?");
            Console.WriteLine("Sélectionnez avec les flèches, puis appuyez sur Entrée pour valider :\n");

            for (int i = 0; i < len; i++)
            {
                if (i == indexSelection)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"> {liste[i]} , Nombre possédé : {GrainesDisponibles[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {liste[i]} , Nombre possédé : {GrainesDisponibles[i]}");
                }
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow && indexSelection > 0)
                indexSelection--;
            else if (key == ConsoleKey.DownArrow && indexSelection < len - 1)
                indexSelection++;

        } while (key != ConsoleKey.Enter);
            Planter(coord, PlantesJouable[indexSelection],jardin);
            NombreAction--;
        }else if (index == 2){
            Recolter(coord);
            NombreAction--;
        }else if (index == 3){
            Deraciner(coord);
            NombreAction--;
        }else if (index == 4){
            Arroser(coord);
            NombreAction--;
        }else if (index == 5){
            Proteger(coord);
            NombreAction--;
        }else if (index == 6){
            Effrayer(coord);
            NombreAction--;
        }else if (index == 7){
            Applaudir(coord);
            NombreAction--;
        }else if (index == 8){
            Console.WriteLine("fonction pas encore implémentée");
            Thread.Sleep(500);
        }else {
            Console.WriteLine("Quel objet voulez vous utiliser ?");
            Thread.Sleep(500);
            string[] liste = ObjectsAchetable;
            int indexSelection = 0;
        int len = liste.Length;
        ConsoleKey key;

        do
        {
            // utilisation d'objet
            Console.Clear();
            Console.WriteLine(jardin);
            Console.WriteLine("Quel objet voulez vous utiliser ?");
            Console.WriteLine("Sélectionnez avec les flèches, puis appuyez sur Entrée pour valider :\n");

            for (int i = 0; i < len; i++)
            {
                if (i == indexSelection)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"> {liste[i]} , Nombre possédé : {Objects[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {liste[i]} , Nombre possédé : {Objects[i]}");
                }
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow && indexSelection > 0)
                indexSelection--;
            else if (key == ConsoleKey.DownArrow && indexSelection < len - 1)
                indexSelection++;

        } while (key != ConsoleKey.Enter);
            Objet(ObjectsAchetable[indexSelection],coord);
        }
    }
    
    // achat et objets
    public void Objet(string choix, int[] coord)
    {
        // actions de chaque objet
        Plantes plante = RechercherPlante(coord);
        if (choix == "Lanterne")
        {
            if (Objects[0] > 0)
            {
                plante.EtatActuel[2] += 3;
                Objects[0]--;
                NombreAction--;
            }
        }
        else if (choix == "Pelle")
        {
            if (Objects[1] > 0)
            {
                MatObjets[coord[0], coord[1]] = 1;
                Recolter(coord);
                Objects[1]--;
                NombreAction--;
            }
        }
        else if (choix == "Écharpe")
        {
            if (Objects[2] > 0)
            {
                Proteger(coord);
                Objects[2]--;
                NombreAction--;
            }
        }
        else if (choix == "Paravent")
        {
            if (Objects[3] > 0)
            {
                Proteger(coord);
                Objects[3]--;
                NombreAction--;
            }
        }
        else if (choix == "Médicament")
        {
            if (Objects[6] > 0)
            {
                foreach (Maladies maladie in plante.Maladie)
                {
                    maladie.Medicament++;
                }
                Objects[6]--;
                NombreAction--;
            }
        }
        else if (choix == "Arrosoir")
        {
            if (Objects[4] > 0)
            {
                plante.EtatActuel[1] += 6;
                Objects[4]--;
                NombreAction--;
            }
        }
        else if (choix == "Pommade")
        {
            if (Objects[7] > 0)
            {
                plante.Explosion = true;
                Objects[7]--;
                NombreAction--;
            }
        }
        else if (choix == "Haut parleur")
        {
            if (Objects[5] > 0)
            {
                MatObjets[coord[0], coord[1]] = 6; 
                plante.EtatActuel[2] += 3;
                Objects[5]--;
                NombreAction--;
            }
        }
    }

    public void AcheterObjet(int objet)
    {
        Objects[objet]++;
    }

    public string[] Ajout(string[] tab, string ajout){
        string[] nouveauTableau = new string[tab.Length + 1];
                        // Copie les éléments
                        for (int i = 0; i < tab.Length; i++)
                        {
                            nouveauTableau[i] = tab[i];
                        }

                        // Ajoute l'élément
                        nouveauTableau[tab.Length - 1] = ajout;
                        return nouveauTableau;
    }
    public void AcheterTerrain(Jardin jardin)
    {
        // achat du terrain
        string[] ob = ["Retour", "Petit Prince", "Businessman", "Buveur", "Vaniteux", "Roi", "Géographe", "Réverbère"];
        int indexSelection = ListeChoix(ob,jardin);
        if (indexSelection != 0)
        {
            if(PoudreEtoile >= 200){
            PlacerTerrain(ob[indexSelection], indexSelection, MatTerrain); // place le tterrians
            // ajout des effets du terrain
            if (indexSelection == 2){
                Terrains ter = new TerrainBusinessman();
                foreach(string plante in ter.PlanteAchetable){
                        PlantesJouable = Ajout(PlantesJouable,plante);
                }
                        MaladiesPossible = Ajout(MaladiesPossible,ter.MaladiesPossible);
                for(int i =0; i<3 ; i++){
                    ListeSaison[i].PourcentMeteos[(int)ter.MeteoPourcentage[3]] = ter.MeteoPourcentage[i];
                }                
            }else if (indexSelection == 3){
                Terrains ter = new TerrainBuveur();
                foreach(string plante in ter.PlanteAchetable){
                    PlantesJouable = Ajout(PlantesJouable,plante);
                }
                        MaladiesPossible = Ajout(MaladiesPossible,ter.MaladiesPossible);
                for(int i =0; i<3 ; i++){
                    ListeSaison[i].PourcentMeteos[(int)ter.MeteoPourcentage[3]] = Convert.ToDouble(ter.MeteoPourcentage[i]);
                }                
            }else if (indexSelection == 4){
                Terrains ter = new TerrainVaniteux();
                foreach(string plante in ter.PlanteAchetable){
                    PlantesJouable = Ajout(PlantesJouable,plante);
                }
                        MaladiesPossible = Ajout(MaladiesPossible,ter.MaladiesPossible);
                for(int i =0; i<3 ; i++){
                    ListeSaison[i].PourcentMeteos[(int)ter.MeteoPourcentage[3]] = Convert.ToDouble(ter.MeteoPourcentage[i]);
                }                
            }else if (indexSelection == 5){
                Terrains ter = new TerrainRoi();
                foreach(string plante in ter.PlanteAchetable){
                    PlantesJouable = Ajout(PlantesJouable,plante);
                }
                        MaladiesPossible = Ajout(MaladiesPossible,ter.MaladiesPossible);
                for(int i =0; i<3 ; i++){
                    ListeSaison[i].PourcentMeteos[(int)ter.MeteoPourcentage[3]] = Convert.ToDouble(ter.MeteoPourcentage[i]);
                }                
            }else if (indexSelection == 6){
                Terrains ter = new TerrainGeographe();
                foreach(string plante in ter.PlanteAchetable){
                   PlantesJouable = Ajout(PlantesJouable,plante);
                }
                        MaladiesPossible = Ajout(MaladiesPossible,ter.MaladiesPossible);
                for(int i =0; i<3 ; i++){
                    ListeSaison[i].PourcentMeteos[(int)ter.MeteoPourcentage[3]] = Convert.ToDouble(ter.MeteoPourcentage[i]);
                }                
            }else if (indexSelection == 6){
                Terrains ter = new TerrainReverbere();
                foreach(string plante in ter.PlanteAchetable){
                    PlantesJouable = Ajout(PlantesJouable,plante);
                }
                        MaladiesPossible = Ajout(MaladiesPossible,ter.MaladiesPossible);
                for(int i =0; i<3 ; i++){
                    ListeSaison[i].PourcentMeteos[(int)ter.MeteoPourcentage[3]] = Convert.ToDouble(ter.MeteoPourcentage[i]);
                }            
            }
            NombreAction--;
            PoudreEtoile-=200;
            }else{
                Console.WriteLine("Vous n'avez pas assez d'argent pour acheter ce terrain.");
                Thread.Sleep(500);
            }
        } else {
            Console.WriteLine("Vous n'avez pas acheté de terrain.");
            Thread.Sleep(500);
        }
    }

    public void PlacerTerrain(string nom, int indice, int[,] matLibre)
    {
        int size = 3;
        int cursorX = 0;
        int cursorY = 0;

        bool found = false;
        for (int y = 0; y < size && !found; y++)
        {
            for (int x = 0; x < size && !found; x++)
            {
                if (matLibre[x, y] == 0)
                {
                    cursorX = x;
                    cursorY = y;
                    found = true;
                }
            }
        }

        if (!found)
        {
            Console.WriteLine("vous ne pouvez plus acheter de terrain.");
            Thread.Sleep(500);
        }
        else
        {
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine($"Sélectionnez une case libre avec les flèches pour choisir l'endroit où vous voulez placer le terrain {nom} puis appuyez sur Entrée :\n");

                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        bool estSelection = (x == cursorX && y == cursorY);
                        bool estLibre = matLibre[x, y] == 0;

                        if (estSelection)
                        {
                            if (estLibre)
                            {
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        else if (!estLibre)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        }

                        Console.Write(matLibre[x, y] == 0 ? " ." : " X");
                        Console.ResetColor();
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }

                key = Console.ReadKey(true).Key;

                int newX = cursorX;
                int newY = cursorY;

                switch (key)
                {
                    case ConsoleKey.LeftArrow: newX = Math.Max(0, cursorX - 1); break;
                    case ConsoleKey.RightArrow: newX = Math.Min(size - 1, cursorX + 1); break;
                    case ConsoleKey.UpArrow: newY = Math.Max(0, cursorY - 1); break;
                    case ConsoleKey.DownArrow: newY = Math.Min(size - 1, cursorY + 1); break;
                }
                // Se déplacer uniquement si la case est libre
                if (matLibre[newX, newY] == 0)
                {
                    cursorX = newX;
                    cursorY = newY;
                }

            } while (key != ConsoleKey.Enter);

            for (int i = 7 * cursorX; i < 7 * (cursorX + 1); i++)
            {
                for (int j = 7 * cursorY; j < 7 * (cursorY+ 1); j++)
                {
                    MatPlante[i,j] = -1;
                    MatAnimaux[i, j] = -1;
                    MatObjets[i, j] = -1;
                }
            }
            MatTerrain[cursorX, cursorY] = indice;

        }
    }

    // fonction pour les différentes actions du joueur

    public void Applaudir(int[] coord)
    {
        // regarde autour et recule tout les animaux qui se trouve sur ses cases et chacun de ceux dérière jusqu'au bord et fait disparaitre l'aniaml si il sort du jardin
        RechercherPlante(coord).EtatActuel[3] += 2;
        if(coord[0] != 0 && coord[0] != 20 && coord[1] != 0 && coord[1] != 20){
        if (MatAnimaux[coord[0] + 1, coord[1]] > 0)
        {
            RechercherPlante([coord[0] + 1, coord[1]]).EtatActuel[3] += 2;
            DeplacementForce(coord, [1, 0]);
        }
        if (MatAnimaux[coord[0], coord[1] + 1] > 0)
        {
            RechercherPlante([coord[0], coord[1] + 1]).EtatActuel[3] += 2;
            DeplacementForce(coord, [0, 1]);
        }
        if (MatAnimaux[coord[0] + 1, coord[1] + 1] > 0)
        {
            RechercherPlante([coord[0] + 1, coord[1] + 1]).EtatActuel[3] += 2;
            DeplacementForce(coord, [1, 1]);
        }
        if (MatAnimaux[coord[0] + 1, coord[1] - 1] > 0)
        {
            RechercherPlante([coord[0] + 1, coord[1] - 1]).EtatActuel[3] += 2;
            DeplacementForce(coord, [1, -1]);
        }
        if (MatAnimaux[coord[0] - 1, coord[1] + 1] > 0)
        {
            RechercherPlante([coord[0] - 1, coord[1] + 1]).EtatActuel[3] += 2;
            DeplacementForce(coord, [-1, 1]);
        }
        if (MatAnimaux[coord[0] - 1, coord[1]] > 0)
        {
            RechercherPlante([coord[0] - 1, coord[1]]).EtatActuel[3] += 2;
            DeplacementForce(coord, [-1, 0]);
        }
        if (MatAnimaux[coord[0], coord[1] - 1] > 0)
        {
            RechercherPlante([coord[0], coord[1] - 1]).EtatActuel[3] += 2;
            DeplacementForce(coord, [0, -1]);
        }
        if (MatAnimaux[coord[0] - 1, coord[1] - 1] > 0)
        {
            RechercherPlante([coord[0] - 1, coord[1] - 1]).EtatActuel[3] += 2;
            DeplacementForce(coord, [-1, -1]);
        }
        }

    }

    public void Effrayer(int[] coord) 
    {
        // suprime tout les animaux autour
        if(coord[0] != 0 && coord[0] != 20 && coord[1] != 0 && coord[1] != 20){
        SupprimerAnimaux([coord[0] + 1, coord[1] + 1]);
        SupprimerAnimaux([coord[0], coord[1]]);
        SupprimerAnimaux([coord[0] - 1, coord[1] - 1]);
        SupprimerAnimaux([coord[0], coord[1] - 1]);
        SupprimerAnimaux([coord[0], coord[1] + 1]);
        SupprimerAnimaux([coord[0] + 1, coord[1] - 1]);
        SupprimerAnimaux([coord[0] - 1, coord[1] + 1]);
        SupprimerAnimaux([coord[0] + 1, coord[1]]);
        SupprimerAnimaux([coord[0] - 1, coord[1]]);
        }
    }
    public void Proteger(int[] coord)
    {
        RechercherPlante(coord).Proteger = true;
    }

    public void Arroser(int[] coord)
    {
        RechercherPlante(coord).EtatActuel[2] += 6;
    }

    public void Planter(int[] coord, string graine, Jardin jardin)
    //Fonction qui permet de planter une graine choisie (donnée en argument) sur une case spécifique (donnée en argument)
    {
        bool planterOk = false;
        for (int i = 0; i < PlantesJouable.Length; i++)
        {
            //On vérifie si la graine a déjà été débloquée par le joueur
            if (PlantesJouable[i] == graine)
            {
                planterOk = true;
            }
        }
        //Etoile, Météorite, Rose, Chapeau, Nuage, Etoile filante, Alcootier, Plante orgueilleuse, Couronne, Planète, Lampadaire
        if (planterOk && MatPlante[coord[0], coord[1]] == -1)
        {
            //on peut planter s'il n'y a pas déjà une plante sur cette case
            if (graine == "Etoile" && GrainesDisponibles[0] > 0)
            {
                //Pour chaque type de graine, si c'est celui sélectionné par le joueur, on créé une nouvelle instance de cette plante et on la place sur MatPlante (en actualisant toutes les variables nécessaires)
                Plantes nouvellePlante = new Etoile(coord, jardin);
                MatPlante[coord[0], coord[1]] = nouvellePlante.Id;
                ListPlante.Add(nouvellePlante);
                nouvellePlante.JourPlanter = TourActuel;
                GrainesDisponibles[0]--;
            }
            else if (graine == "Météorite" && GrainesDisponibles[1] > 0)
            {
                Plantes nouvellePlante = new Meteorite(coord, jardin);
                MatPlante[coord[0], coord[1]] = nouvellePlante.Id;
                ListPlante.Add(nouvellePlante);
                nouvellePlante.JourPlanter = TourActuel;
                GrainesDisponibles[1]--;
            }
            else if (graine == "Rose" && GrainesDisponibles[2] > 0)
            {
                Plantes nouvellePlante = new Rose(coord, jardin);
                MatPlante[coord[0], coord[1]] = nouvellePlante.Id;
                ListPlante.Add(nouvellePlante);
                nouvellePlante.JourPlanter = TourActuel;
                GrainesDisponibles[2]--;
            }
            else if (graine == "Chapeau" && GrainesDisponibles[3] > 0)
            {
                Plantes nouvellePlante = new Chapeau(coord, jardin);
                MatPlante[coord[0], coord[1]] = nouvellePlante.Id;
                ListPlante.Add(nouvellePlante);
                nouvellePlante.JourPlanter = TourActuel;
                GrainesDisponibles[3]--;
            }
            else if (graine == "Nuage" && GrainesDisponibles[4] > 0)
            {
                Plantes nouvellePlante = new Nuage(coord, jardin);
                MatPlante[coord[0], coord[1]] = nouvellePlante.Id;
                ListPlante.Add(nouvellePlante);
                nouvellePlante.JourPlanter = TourActuel;
                GrainesDisponibles[4]--;
            }
            else if (graine == "Etoile filante" && GrainesDisponibles[5] > 0)
            {
                Plantes nouvellePlante = new EtoileFilante(coord, jardin);
                MatPlante[coord[0], coord[1]] = nouvellePlante.Id;
                ListPlante.Add(nouvellePlante);
                nouvellePlante.JourPlanter = TourActuel;
                GrainesDisponibles[5]--;
            }
            else if (graine == "Alcootier" && GrainesDisponibles[6] > 0)
            {
                Plantes nouvellePlante = new Alcootier(coord, jardin);
                MatPlante[coord[0], coord[1]] = nouvellePlante.Id;
                ListPlante.Add(nouvellePlante);
                nouvellePlante.JourPlanter = TourActuel;
                GrainesDisponibles[6]--;
            }
            else if (graine == "Plante orgueilleuse" && GrainesDisponibles[7] > 0)
            {
                Plantes nouvellePlante = new PlanteOrgueilleuse(coord, jardin);
                MatPlante[coord[0], coord[1]] = nouvellePlante.Id;
                ListPlante.Add(nouvellePlante);
                nouvellePlante.JourPlanter = TourActuel;
                GrainesDisponibles[7]--;
            }
            else if (graine == "Couronne" && GrainesDisponibles[8] > 0)
            {
                Plantes nouvellePlante = new Couronne(coord, jardin);
                MatPlante[coord[0], coord[1]] = nouvellePlante.Id;
                ListPlante.Add(nouvellePlante);
                nouvellePlante.JourPlanter = TourActuel;
                GrainesDisponibles[8]--;
            }
            else if (graine == "Planète" && GrainesDisponibles[9] > 0)
            {
                Plantes nouvellePlante = new Planete(coord, jardin);
                MatPlante[coord[0], coord[1]] = nouvellePlante.Id;
                ListPlante.Add(nouvellePlante);
                nouvellePlante.JourPlanter = TourActuel;
                GrainesDisponibles[9]--;
            }
            else if (graine == "Lampadaire" && GrainesDisponibles[10] > 0)
            {
                Plantes nouvellePlante = new Lampadaire(coord, jardin);
                MatPlante[coord[0], coord[1]] = nouvellePlante.Id;
                ListPlante.Add(nouvellePlante);
                nouvellePlante.JourPlanter = TourActuel;
                GrainesDisponibles[10]--;
            }
            else { Console.WriteLine("Cette plante n'est pas encore disponible ou vous n'avez pas assez de graines."); NombreAction++; Thread.Sleep(1000); }
        }
        else
        {
            Console.WriteLine("vous n'avez pas la place pour planter ici !");
            NombreAction++;
            Thread.Sleep(2000);
        }
    }
    public void Deraciner(int[] coord)
    {
        SupprimerPlante(coord);

    }
    public void Recolter(int[] coord)
    {
        //Fonction pour récolter une plante. Si la plante est monocarpique, elle disparait après avoir été récoltée. Si elle est polycarpique, elle est remise à zéro et peu de nouveau faire pousser un fruit
        Plantes planteRecoltee = RechercherPlante(coord);
        if (TourActuel - planteRecoltee.JourPlanter >= planteRecoltee.Croissance * 30)
        //Si la plante n'avait pas encore fait de fruit (n'était pas arrivée à maturation), on annule l'action du joueur 
        {
            if (planteRecoltee.IdFruit >= 0)
            {
                //On vérifie si c'est un type de plante qui fait des fruits
                Objects[planteRecoltee.IdFruit] += planteRecoltee.Produit;
            }
            if (planteRecoltee.Nature == "Monocarpique")
            {
                SupprimerPlante(coord);
            }
            else
            {
                planteRecoltee.JourPlanter = TourActuel;
            }
        }else{
            Console.WriteLine("Votre plante n'a pas encore grandi assez pour la recolter.");
            Thread.Sleep(1000);
        }

    }

    public void DeplacementForce(int[] coord, int[] direction)
    {
        // recule l'animal recursivement avec les animaux dans la direction voulu et suprime si sort du jardin
        if(coord[0] != 0 && coord[0] != 20 && coord[1] != 0 && coord[1] != 20){
        if (MatAnimaux[coord[0] + direction[0], coord[1] + direction[1]] > 0)
        {
            DeplacementForce([coord[0] + direction[0], coord[1] + direction[1]], direction);
            MatAnimaux[coord[0] + direction[0], coord[1] + direction[1]] = RechercherAnimaux([coord[0] + direction[0], coord[1] + direction[1]]).Id;
        }
        else
        {
            MatAnimaux[coord[0] + direction[0], coord[1] + direction[1]] = RechercherAnimaux([coord[0] + direction[0], coord[1] + direction[1]]).Id;
        }
        }
    }
    // fonction de recherche globale
    public Plantes RechercherPlante(int[] co)
    //Recherche et renvoie la plante présente sur les coordonnées de la case données en argument. S'il n'y avait pas de plante, on renvoie une plante vide fabriquée
    {
        int indiceCherche = MatPlante[co[0], co[1]];
        foreach (Plantes plante in ListPlante)
        {
            if (plante.Id == indiceCherche) { return plante; }
        }
        return new PlanteVide(new Jardin());  
    }

    public Animaux RechercherAnimaux(int[] co)
    {
        //Recherche et renvoie l'animal présent sur les coordonnées de la case données en argument. S'il n'y avait pas d'animal, on renvoie un animal vide fabriqué
        int indiceCherche = MatAnimaux[co[0], co[1]];
        foreach (Animaux animal in ListAnimaux)
        {
            if (animal.Id == indiceCherche)
            { return animal; }
        }
        return new Animaux("",[],0,new Jardin(), "  ");
    }
    
    public int[] RechercherPlanteProche(int[] coOrigine, string planteCherchee) { //planteCherchee correspond au nom de la plante la plus proche recherchée. Si on veut regarder toutes les plantes, mettre ""
        //Recherche la plante d'un certain type la plus proche d'une certaine case (dont les coordonnées sont donnés en argument). Renvoie les coordonnées de la plante la plus proche
        int[] coPlanteProche = [coOrigine[0], coOrigine[1]];
        double distPlanteProche = 1000;
        for (int i = 0; i < 21; i++) {
            for (int j = 0; j < 21; j++) {
                if (MatPlante[i, j] > 0 && (planteCherchee=="" || RechercherPlante([i,j]).Nom==planteCherchee)) {
                    double dist = Math.Sqrt(Math.Pow(coOrigine[0] - i, 2) + Math.Pow(coOrigine[1] - j, 2));
                    //On détermine la plante la plus proche à l'aide de la distance √((x2 - x1)² + (y2 - y1)²)
                    if (dist < distPlanteProche)
                    {
                        distPlanteProche = dist;
                        coPlanteProche = [i, j];
                    }
                }
            }
        }
        return coPlanteProche;
    }

    public int[] RechercherAnimalProche(int[] coOrigine, string animalCherche) { //animalCherche correspond à l'espèce de l'animal le plus proche recherché. Si on veut regarder tous les animaux, mettre ""
        //Recherche l'animal d'une certaine espèce le plus proche d'une certaine case (dont les coordonnées sont donnés en argument). Renvoie les coordonnées de l'animal le plus proche
        int[] coAnimalProche = [coOrigine[0], coOrigine[1]];
        double distAnimalProche = 1000;
        for (int i = 0; i < 21; i++) {
            for (int j = 0; j < 21; j++) {
                if (MatAnimaux[i, j] > 0 && (animalCherche=="" || RechercherAnimaux([i,j]).Nom==animalCherche)) {
                    double dist = Math.Sqrt(Math.Pow(coOrigine[0] - i, 2) + Math.Pow(coOrigine[1] - j, 2));
                    if (dist < distAnimalProche) {
                        distAnimalProche = dist;
                        coAnimalProche = [i, j];
                    }
                }
            }
        }
        return coAnimalProche;
    }

    public int[] DeplacementDirigeAnimaux(int[] coAnimal, int[] coCible)
    {
        //On fourni en argument les coordonnées d'un animal et les coordonnées de la case vers laquelle il souhaite se diriger. La fonction renvoie une liste Deplacement, qui indique le déplacement à faire en x et en y pour aller vers cette case (déplacement à additionner aux cos de l'animal pour le déplacer)
        int diffX = coCible[0] - coAnimal[0];
        int diffY = coCible[1] - coAnimal[1];
        int[] deplacement = [0, 0];
        if (diffX != 0 && diffY != 0) { deplacement = [diffX / Math.Abs(diffX), diffY / Math.Abs(diffY)]; }
        else if (diffY != 0) { deplacement = [0, diffY / Math.Abs(diffY)]; }
        else if (diffX != 0) { deplacement = [diffX / Math.Abs(diffX), 0]; }
        
        if(diffY != 0 && diffX != 0){
        if (MatAnimaux[coAnimal[0] + deplacement[0], coAnimal[1] + deplacement[1]] != -1)
        {
            //Si un autre animal est déjà présent sur la case ou on souhaite se déplacer, on regarde essaie de se déplacer sur une case adjacente
            if (diffY != 0)
                {
                    if (diffY / Math.Abs(diffY) <= 0)
                    {
                        if (MatAnimaux[coAnimal[0] + deplacement[0], coAnimal[1] + deplacement[1] + 1] != -1) { deplacement = [0, 0]; }
                        //Si un autre animal est aussi présent sur la case adjacente, on ne se déplacera pas (déplacement mis à [0, 0])
                        else { deplacement[1] = deplacement[1] + 1; }
                    }
                    else
                    {
                        if (MatAnimaux[coAnimal[0] + deplacement[0], coAnimal[1] + deplacement[1] - 1] != -1) { deplacement = [0, 0]; }
                        else { deplacement[1] = deplacement[1] - 1; }
                    }
                }
                else
                {
                    if (diffX / Math.Abs(diffX) <= 0)
                    {
                        if (MatAnimaux[coAnimal[0] + deplacement[0] - 1, coAnimal[1] + deplacement[1]] != -1) { deplacement = [0, 0]; }
                        else { deplacement[0] = deplacement[0] - 1; }
                    }
                    else
                    {
                        if (MatAnimaux[coAnimal[0] + deplacement[0] + 1, coAnimal[1] + deplacement[1]] != -1) { deplacement = [0, 0]; }
                        else { deplacement[0] = deplacement[0] + 1; }
                    }
                }

        }
        }
        return deplacement;
    }

    // fonction de suppression 
    public void SupprimerPlante(int[] co)
    {
        //Supprime la plante située sur la case dont les coordonnées sont données en argument. On retire la plante de la liste ListePlante et on met un -1 (=une case vide) à la place de son id sur la matrice MatPlante
        Plantes planteSup = RechercherPlante(co);
        if (planteSup.Nom != "")
        {
            ListPlante.Remove(planteSup);
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    if (MatPlante[i, j] == planteSup.Id)
                    {
                        MatPlante[i, j] = -1;
                    }
                }
            }
        }
    }

    public void SupprimerAnimaux(int[] co)
    {
        //Supprime l'animal situé sur la case dont les coordonnées sont données en argument
        Animaux animalSup = RechercherAnimaux(co);
        if (animalSup.Nom != "")
        {
            ListAnimaux.Remove(animalSup);
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    if (MatAnimaux[i, j] == animalSup.Id)
                    {
                        MatAnimaux[i, j] = -1;
                    }
                }
            }
        }
    }


    public string AfficherJardins()
    {
        Console.WriteLine("");
        string resultat = "||=========================================||=========================================||=========================================||\n";
        for (int j = 0; j < 3; j++)
        {
            for (int k = 0; k < 7; k++)
            {

                resultat += "||-----------------------------------------||-----------------------------------------||-----------------------------------------||\n||";
                for (int i = 0; i < 3; i++)
                {
                    for (int m = 0; m < 7; m++)
                    {

                        string données = Convert.ToString(MatPlante[i * 7 + m, j * 7 + k]);
                        if (MatPlante[i * 7 + m, j * 7 + k] == 0)
                        {
                            resultat += "  >< |";
                        }
                        else if (MatPlante[i * 7 + m, j * 7 + k] == -1 && MatAnimaux[i * 7 + m, j * 7 + k] == -1)
                        {
                            resultat += "     |";
                        }
                        else if (MatPlante[i * 7 + m, j * 7 + k] == -1)
                        {
                            resultat += "  " + RechercherAnimaux([i * 7 + m, j * 7 + k]).Emoji + " |";
                        }
                        else if (MatAnimaux[i * 7 + m, j * 7 + k] == -1)
                        {
                            if (MatPlante[i * 7 + m, j * 7 + k] == 1)
                            {
                                resultat += "  ✨ |";
                            }
                            else
                            {
                                resultat += "  " + RechercherPlante([i * 7 + m, j * 7 + k]).Emoji + " |";
                            }
                        }
                        else
                        {
                            if (MatPlante[i * 7 + m, j * 7 + k] == 1)
                            {
                                resultat += "✨ " + RechercherPlante([i * 7 + m, j * 7 + k]).Emoji + "|";
                            }
                            else
                            {
                                resultat += RechercherAnimaux([i * 7 + m, j * 7 + k]).Emoji + " " + RechercherPlante([i * 7 + m, j * 7 + k]).Emoji + "|";
                            }
                        }
                    }
                    resultat += "|";
                }
                resultat += "\n";
            }
            resultat += "||-----------------------------------------||-----------------------------------------||-----------------------------------------||\n||=========================================||=========================================||=========================================||\n";
        }
        return resultat;
    }

    public override string ToString()
    {
        string reponse = AfficherJardins();
        reponse += $"\n\n jour : {TourActuel} Nombre d'action : {NombreAction} poussière d'étoiles : {PoudreEtoile} ";
        return reponse;
    }
}

