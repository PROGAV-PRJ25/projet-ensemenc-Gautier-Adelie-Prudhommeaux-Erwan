using System;

public class Jardin
{
    public int TourActuel { get; set; }
    public int Poudredetoile { get; set; }
    public List<Saisons> ListeSaison { get; set; }
    public Saisons Saison { get; set; }
    public Meteos Meteo { get; set; }
    public int[,] MatPlante { get; set; }
    public int[,] MatAnimaux { get; set; }
    public int[,] MatObjets { get; set; }
    public string[] ActionPossible { get; set; }
    public int NombreAction { get; set; }
    public int[] Objects { get; set; }// chaque indice correspond a un object. il y en a 25 
    public string[] PlantesJouable { get; set; }
    public string[] MaladiesPossible { get; set; }
    public string[] ObjectsAchetable { get; set; }
    public int[] GrainesDisponibles { get; set; } // chaque indice est associé a une plante. il y en a 11 
    public List<Plantes> ListPlante { get; set; }
    public List<Animaux> ListAnimaux { get; set; }
    public int[,] MatTerrain { get; set; }




    public Jardin()
    {
        TourActuel = 0;
        NombreAction = 3;
        Poudredetoile = 100;
        Meteo = new Calme();
        ListeSaison = new List<Saisons> { new Saison1(), new Saison2(), new Saison3() };
        Saison = ListeSaison[0];
        PlantesJouable = ["Etoile", "Météorite", "Rose", "Chapeau", "Nuage"];
        ActionPossible = ["Objet", "Planter", "Récolter", "Déraciné", "Arroser", "Protéger", "Effrayer", "Applaudir"];
        MaladiesPossible = ["Maladie1"];
        ObjectsAchetable = ["Lanterne", "Pelle", "Écharpe", "Paravent", "Arrosoir", "Haut parleur", "Médicament", "Pommade"]; //cloture et epouventails
        Objects = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
        GrainesDisponibles = [10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
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
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                MatPlante[i, j] = -1;
                MatAnimaux[i, j] = -1;
                MatObjets[i, j] = -1;
            }
        }
    }

    // magasin et objets
    public void Objet(string choix, int[] coord)
    {
        Plantes plante = RechercherPlante(coord);
        if (choix == "Lanterne")
        {
            if (Objects[0] > 0)
            {
                plante.EtatActuel[2] += 3;
                Objects[0]--;
            }
        }
        else if (choix == "Pelle")
        {
            if (Objects[1] > 0)
            {
                MatObjets[coord[0], coord[1]] = 1;
                Recolter(coord);
                Objects[1]--;
            }
        }
        else if (choix == "Écharpe")
        {
            if (Objects[2] > 0)
            {
                Proteger(coord);
                Objects[2]--;
            }
        }
        else if (choix == "Paravent")
        {
            if (Objects[3] > 0)
            {
                Proteger(coord);
                Objects[3]--;
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
            }
        }
        else if (choix == "Arrosoir")
        {
            if (Objects[4] > 0)
            {
                plante.EtatActuel[1] += 6;
                Objects[4]--;
            }
        }
        else if (choix == "Pommade")
        {
            if (Objects[7] > 0)
            {
                plante.Explosion = true;
                Objects[7]--;
            }
        }
        else if (choix == "Haut parleur")
        {
            if (Objects[5] > 0)
            {
                MatObjets[coord[0], coord[1]] = 6; // a diminuer quand un tour passe.
                plante.EtatActuel[2] += 3;
                Objects[5]--;
            }
        }
    }

    public void AcheterObjet(int objet)
    {
        Objects[objet - 1]++;
    }
    public void AcheterTerrain(string[] ob)
    {
        int indexSelection = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine("Sélectionnez un mot avec les flèches, puis appuyez sur Entrée :\n");

            for (int i = 0; i < options.Count; i++)
            {
                if (i == indexSelection)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"> {ob[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {ob[i]}");
                }
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.LeftArrow && indexSelection > 0)
                indexSelection--;
            else if (key == ConsoleKey.RightArrow && indexSelection < options.Count - 1)
                indexSelection++;

        } while (key != ConsoleKey.Enter);
        PlacerTerrain(ob[indexSelection], indexSelection, MatTerrain);
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
                if (matrice[x, y] == 0)
                {
                    cursorX = x;
                    cursorY = y;
                    found = true;
                }
            }
        }

        if (!found)
            console.WriteLine("vous ne pouvez plus acheter de terrain.");

        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine($"Sélectionnez une case libre avec les flèches pour sélectionnés l'endroit ou vous voulez placer le terrain {nom} puis appuyez sur Entrée :\n");

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
            for (int j = 7 * cursorY; j < 7 * (cursorX + 1); j++)
            {
                MatPlante[i, j] = -1;
                MatAnimaux[i, j] = -1;
                MatObjets[i, j] = -1;
            }
        }
        MatTerrain[cursorX, cursorY] = indice;

    }

    // fonction pour les différentes actions du joueur

    public void Applaudir(int[] coord)
    {
        RechercherPlante(coord).EtatActuel[3] += 2;
        if (MatAnimaux[coord[0] + 1, coord[1]] > 0)
        {
            RechercherPlante([coord[0] + 1, coord[1]]).EtatActuel[3] += 2;
            deplacementForcé(coord, [1, 0]);
        }
        if (MatAnimaux[coord[0], coord[1] + 1] > 0)
        {
            RechercherPlante([coord[0], coord[1] + 1]).EtatActuel[3] += 2;
            deplacementForcé(coord, [0, 1]);
        }
        if (MatAnimaux[coord[0] + 1, coord[1] + 1] > 0)
        {
            RechercherPlante([coord[0] + 1, coord[1] + 1]).EtatActuel[3] += 2;
            deplacementForcé(coord, [1, 1]);
        }
        if (MatAnimaux[coord[0] + 1, coord[1] - 1] > 0)
        {
            RechercherPlante([coord[0] + 1, coord[1] - 1]).EtatActuel[3] += 2;
            deplacementForcé(coord, [1, -1]);
        }
        if (MatAnimaux[coord[0] - 1, coord[1] + 1] > 0)
        {
            RechercherPlante([coord[0] - 1, coord[1] + 1]).EtatActuel[3] += 2;
            deplacementForcé(coord, [-1, 1]);
        }
        if (MatAnimaux[coord[0] - 1, coord[1]] > 0)
        {
            RechercherPlante([coord[0] - 1, coord[1]]).EtatActuel[3] += 2;
            deplacementForcé(coord, [-1, 0]);
        }
        if (MatAnimaux[coord[0], coord[1] - 1] > 0)
        {
            RechercherPlante([coord[0], coord[1] - 1]).EtatActuel[3] += 2;
            deplacementForcé(coord, [0, -1]);
        }
        if (MatAnimaux[coord[0] - 1, coord[1] - 1] > 0)
        {
            RechercherPlante([coord[0] - 1, coord[1] - 1]).EtatActuel[3] += 2;
            deplacementForcé(coord, [-1, -1]);
        }

    }

    public void Effrayer(int[] coord)
    {
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
    public void Proteger(int[] coord)
    {
        RechercherPlante([coord[0], coord[1]]).Proteger = true;
    }

    public void Arroser(int[] coord)
    {

    }
    public void Planter(int[] coord)
    {

    }
    public void Deraciner(int[] coord)
    {

    }
    public void Recolter(int[] coord)
    {

    }

    public void deplacementForcé(int[] coord, int[] direction)
    {
        if (MatAnimaux[coord[0] + direction[0], coord[1] + direction[1]] > 0)
        {
            deplacementForcé([coord[0] + direction[0], coord[1] + direction[1]], direction);
            MatAnimaux[coord[0] + direction[0], coord[1] + direction[1]] = RechercherAnimaux([coord[0] + direction[0], coord[1] + direction[1]]).Id;
        }
        else
        {
            MatAnimaux[coord[0] + direction[0], coord[1] + direction[1]] = RechercherAnimaux([coord[0] + direction[0], coord[1] + direction[1]]).Id;
        }
    }
    // fonction de recherche globale
    public Plantes RechercherPlante(int[] co)
    {
        int indiceCherché = MatPlante[co[0], co[1]];
        foreach (Plantes plante in ListPlante)
        {
            if (plante.Id == indiceCherché) { return plante; }
        }
        return new Plantes("", [], "", false, new Saison1(), "", 0, [], [], 0, 0, new List<Maladies> { }, 0, 0, "", new Jardin());
    }

    public Animaux RechercherAnimaux(int[] co)
    {
        int indiceCherché = MatAnimaux[co[0], co[1]];
        foreach (Animaux animal in ListAnimaux)
        {
            if (animal.Id == indiceCherché)
            { return animal; }
        }
        return new Animaux("", [], 0, 0, new List<int> { }, new Jardin(), "");
    }

    public int[] DeplacementDirigéAnimaux(int[] coAnimal, int[] coCible)
    {
        int diffX = coCible[0] - coAnimal[0];
        int diffY = coCible[1] - coAnimal[1];
        int[] deplacement = [0, 0];
        if (diffX != 0 && diffY != 0) { deplacement = [diffX / Math.Abs(diffX), diffY / Math.Abs(diffY)]; }
        else if (diffY != 0) { deplacement = [0, diffY / Math.Abs(diffY)]; }
        else if (diffX != 0) { deplacement = [diffX / Math.Abs(diffX), 0]; }

        if (MatAnimaux[coAnimal[0] + deplacement[0], coAnimal[1] + deplacement[1]] != -1)
        {
            if (diffY / Math.Abs(diffY) <= 0)
            {
                if (MatAnimaux[coAnimal[0] + deplacement[0], coAnimal[1] + deplacement[1] + 1] != -1) { deplacement = [0, 0]; }
                else { deplacement[1] = deplacement[1] + 1; }
            }
            else
            {
                if (MatAnimaux[coAnimal[0] + deplacement[0], coAnimal[1] + deplacement[1] - 1] != -1) { deplacement = [0, 0]; }
                else { deplacement[1] = deplacement[1] - 1; }
            }
        }
        return deplacement;
    }

    // fonction de supprésion 
    public void SupprimerPlante(int[] co)
    {
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
        string resultat = "||=========================================||=========================================||=========================================||\n";
        for (int i = 0; i < 3; i++)
        {
            for (int m = 0; m < 7; m++)
            {

                resultat += "||-----------------------------------------||-----------------------------------------||-----------------------------------------||\n||";
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 7; k++)
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
        return AfficherJardins();
    }
}

