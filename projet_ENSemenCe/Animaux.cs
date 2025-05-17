public class Animaux {
    public string Nom {get; set;}
    public int[,] Position {get; set;}  //[ligne, colonne]
    public int PlaceOccupée {get; set;}
    public int Groupe {get; set;}
    public List<int> TauxApparition {get; set;}
    public int Id {get;set;}
    public static int IdSuivant = 1;

    public Animaux(string nom, int[,] position, int placeOccupée, int groupe, List<int> tauxApparition) {
        Nom = nom;
        Position = position;
        PlaceOccupée = placeOccupée;
        Groupe = groupe;
        TauxApparition = tauxApparition;
        Id = IdSuivant;
        IdSuivant ++;
    }
}


//------------------------------------ Animaux amicaux -------------------------------------

public class Serpent : Animaux {
    public bool Caché {get; set;}
    public Serpent(int[,] position) : base("Serpent", position, 1, 1, new List<int> {0, 0, 0, 0, 0, 0, 0}) {
        Caché = false;
    }

    public void Deplacer() {
        //Si elephant sur une case adjacente, il va le manger
        //Si un chapeau sur une case adjacente, il va se cacher en dessous
        //Sinon, se dirige vers l'elephant ou le chapeau le plus proche, ou se déplace aléatoirement
    }
}

public class Mouton : Animaux {
    public Mouton(int[,] position) : base("Mouton", position, 1, 0, new List<int> {0, 0, 0, 0, 0, 0, 0}) {
        Random aleatoire = new Random();
        Groupe = aleatoire.Next(1,3);
    }

    public void Deplacer() {
        //Si baobab sur une case adjacente, il va le manger
        //Sinon, se dirige vers le baobab ou le mouton le plus proche, ou se déplace aléatoirement
    }
}


//------------------------------------ Animaux nuisibles -------------------------------------

public class Elephant : Animaux {
    public int[] Dégats {get; set;}
    public int Direction {get; set;}
    public Elephant(int[,] position) : base("Éléphant", position, 2, 1, new List<int> {0, 0, 0, 0, 0, 0, 0}) {
        Dégats = [-2000, 0, 0 , 0];
        Random aleatoire = new Random();
        Direction = aleatoire.Next(4);
        //0:N  1:S, 2:E, 3:O
    }
        
    public void Deplacer() {
        //Se déplace en ligne droite
        modelisationAnimaux.Matrix[position] = 0;
        if (Direction == 1) {
            if (position[0] == 6) {
                if (modelisationAnimaux.Sud != null) {
                    Position = [0, Position[1]];
                    modelisationAnimaux.Sud.Matrix[position] = Id;
                }
                else {ListAnimaux}
            }
            else {
                Position = [Position[0]+1, Position[1]];
                modelisationAnimaux.Matrix[position] = Id;
            }
            
        }
        else if (Direction == 1) {

        }
        else if (Direction == 2) {

        }
        else {

        }
    }
}

public class Oiseau : Animaux {
    public int[] Dégats {get; set;}
    public Oiseau(int[,] position) : base("Oiseau", position, 1, 0, new List<int> {0, 0, 0, 0, 0, 0, 0}) {
        Random aleatoire = new Random();
        Groupe = aleatoire.Next(2,4);
        Dégats = [-5, 0, 0 , 0];
    }

    public void Deplacer() {
        //Si plante sur une case adjacente, il va la picorer --> enlève des pv à la plante (définir un nb de pv enlevé)
        //Sinon, se dirige vers la plante la plus proche, ou se déplace aléatoirement
    }
}