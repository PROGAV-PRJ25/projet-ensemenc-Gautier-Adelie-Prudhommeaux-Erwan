//penser a trier les classes
public class Terrains{
    public string Nomplanete {get;set;}
    public List<Maladies>? MaladiesPossible {get; set;}
    public List<Plantes>? PlanteAchetable {get;set;}
    public Meteos? MeteoAjouté {get;set;} 


    public Terrains(int numplanete, List<Maladies> maladiesPossible, List<Plantes> planteAchetable, Meteos meteoAjouté){
        string[] nomTerrains = ["Aucun","Petit Prince","Businessman","Buveur","Vaniteux","Roi","Géographe","Réverbère"];
        Nomplanete = nomTerrains[numplanete];
        MaladiesPossible = maladiesPossible;
        PlanteAchetable = planteAchetable;
        MeteoAjouté = meteoAjouté;
    }
}

public class TerrainPetitPrince:Terrains{
    public TerrainPetitPrince():base(1,new List<Maladies> {},new List<Plantes> {},new Meteos(0)){}
}
public class TerrainBusinessman:Terrains{
    public TerrainBusinessman():base(2,new List<Maladies> {},new List<Plantes> {},new Meteos(1)){}
}
public class TerrainBuveur:Terrains{
    public TerrainBuveur():base(3,new List<Maladies> {},new List<Plantes> {},new Meteos(3)){}
}
public class TerrainRoi:Terrains{
    public TerrainRoi():base(4,new List<Maladies> {},new List<Plantes> {},new Meteos(4)){}
}
public class TerrainGéographe:Terrains{
    public TerrainGéographe():base(5,new List<Maladies> {},new List<Plantes> {},new Meteos(5)){}
}
public class TerrainRéverbère:Terrains{
    public TerrainRéverbère():base(6,new List<Maladies> {},new List<Plantes> {},new Meteos(6)){}
}


public class Meteos{
    public string Nom {get;set;}
    public int NuméroCata {get;set;} // 0 pas de catastrrphe sinon numéro de la catastrophes a definir pour chaque
    public Meteos(string nom, int cata){
        Nom = nom;
        NuméroCata = cata;
    }
} 
public class Calme : Meteos {
    public Calme():base("Calme",0){
    }
}
public class Pluie : Meteos {
    public Pluie():base("Pluie",0){
    }
}
public class Nuit : Meteos {
    public Nuit():base("Nuit",0){
    }
}
public class Soleil : Meteos {
    public Soleil():base("Soleil",0){
    }
}
public class Secheresse : Meteos{
    public Secheresse():base("Secheresse",1){
    }
}
public class Gel : Meteos {
    public Gel():base("Gel",2){
    }
}
public class TempêteStellaire : Meteos {
    public TempêteStellaire():base("Tempête Stellaire",2){
    }
}
public class CriseEconomique : Meteos {
    public CriseEconomique():base("Crise économique",3){
    }
}
public class FortePluie : Meteos {
    public FortePluie():base("Forte Pluie",4){
    }
}
public class HordeAnimaux : Meteos {
    public HordeAnimaux():base("Horde d’Animaux",5){
    }
}
public class Obligation : Meteos {
    public Obligation():base("Obligation",6){
    }
}
public class ChangementSaison : Meteos {
    public ChangementSaison():base("Changement de saison",7){
    }
}
public class ToutNoir : Meteos {
    public ToutNoir():base("Tout Noir",8){
    }
}

public class Saisons {
    public string Nom {get;set;}
    public int[] PourcentMeteos {get;set;}
    public Saison(string nom, int[] pourcentMeteos){
        Nom = nom;
        PourcentMeteos = pourcentMeteos;
    }
}

public class Saison1 : Saisons{
    public Saison1():base("Saison1",[20,20,20,20,8,5,1,0,0,0,0,0,0])//[20,20,20,20,8,5,1,1,1,1,1,1,1]
}
public class Saison2 : Saisons{
    public Saison2():base("Saison2",[20,20,20,20,5,8,1,0,0,0,0,0,0])//[20,20,20,20,5,8,1,1,1,1,1,1,1]
}
public class Saison3 : Saisons{
    public Saison3():base("Saison3",[50,15,15,15,0,0,0.8,0,0,0,0,0,0])//[50,15,15,15,0,0,0.8,0.7,0.7,0.7,0.7,0.7,0.7]
}
public class Jardin{
    public int TourActuel {get;set;}
    public int Argent {get;set;}
    public Saisons Saison {get;set;}
    public Meteos Meteo {get;set;}
    public MatrixNode Modelisation {get;set;} // définir a chaque numéro une plante ou un animal ou objet 
    public string[] ActionPossible {get;set;}
    public int NombreAction {get;set;}
    public int[] Objects {get;set;}// chaque indice correspond a un object. il y en a 23 

    public string[] PlantesJouable {get;set;}
    public string[] MaladiesPossible {get;set;}
    public string[] ObjectsAchetable {get;set;} 
     
    public int[] GrainesDisponibles {get;set;} // chaque indice est assosier a une plante. il y en a 11 
    
    


    public Jardin(){
        TourActuel = 0;
        Argent = 100;
        PlantesJouable = ["Etoile", "Météorite", "Rose", "Chapeau", "Nuage"];
        MaladiesPossible = ["Maladie1"];
        ObjectsAchetable = ["Lanterne","Pelle","Écharpe","Paravent","Clôture","Tuyaux d'arrosage","Épouventails","Haut parleur"];
        Objects = [0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0];
        GrainesDisponibles = [10,0,0,0,0,0,0,0,0,0,0];
        int[,] mat = new int[7,7];
        Modelisation = new MatrixNode(mat , 1);
    }
}


public class MatrixNode
{
    public int[,] Matrix { get; set; }
    public int planete { get; set; }
    public MatrixNode Nord { get; set; } 
    public MatrixNode Sud { get; set; }
    public MatrixNode Est  { get; set; }
    public MatrixNode Ouest  { get; set; }

    public MatrixNode(int[,] matrix, int numplanete)
    {
        Matrix = matrix;
        planete = numplanete;
    }


    public void AddMatrix(MatrixNode origin, int[,] newMatrix, string direction, int numplanete) {
        MatrixNode newNode = new MatrixNode(newMatrix,numplanete);

        switch (direction.ToLower())
        {
            case "Nord":
                origin.Nord = newNode;
                newNode.Sud = origin;
                break;
            case "Sud":
                origin.Sud = newNode;
                newNode.Nord = origin;
                break;
            case "Est":
                origin.Est = newNode;
                newNode.Ouest = origin;
                break;
            case "Ouest":
                origin.Ouest = newNode;
                newNode.Est = origin;
                break;
            default:
                throw new ArgumentException("Direction invalide");
        }
    }
}
public class Plantes {
    public string Nom {get; set;}
    public int[,] Position {get; set;}
    public string Nature {get; set;}
    public bool Comestible {get; set;}
    public Saisons Saison {get; set;}
    public Terrains Terrain {get; set;}
    public int Place {get; set;}
    public int[] Besoin {get; set;}  //[eaumin, eaumax, lumièremin, applaudissementmin]
    public int[] EtatActuel {get; set;}  //[vie, eau, lumière, applaudissement]
    public int Longevite {get; set;}  //En mois
    public int Produit {get; set;}
    public List<Maladies> Maladie {get; set;}  //les maladies qu'a la plante
    public int Croissance {get; set;}  //Temps de croissance en mois
    public int Hauteur {get; set;}
    static int Indice {get;set;}


    public Plantes(string nom, int[,] position, string nature, bool comestible, Saisons saison, Terrains terrain, int place, int[] besoin, int[] etatActuel, int longevite, int produit, List<Maladies> maladie, int croissance, int hauteur) {
        Nom = nom;
        Position = position;
        Nature = nature;
        Comestible = comestible;
        Saison = saison;
        Terrain = terrain;
        Place = place;
        Besoin = besoin;
        EtatActuel = etatActuel;
        Longevite = longevite;
        Produit = produit;
        Maladie = maladie;
        Croissance = croissance;
        Hauteur = hauteur;
    }
}

public class Etoile : Plantes {
    public Etoile(int[,] position) : base("Etoile", position, "Polycarpique", false, new Saisons(0), new Terrains(0), 1, [100,150,150,0],[100,125,160,0], 12, 1, new List<Maladies> {}, 1, 3) {}
} //A voir si on enleve les positions (pas utile)

public class Meteorite : Plantes {
    public Meteorite(int[,] position) : base("Météorite", position, "Monocarpique", false, new Saisons(1), new Terrains(1), 1, [], [150], 24, 0, new List<Maladies> {}, 6, 2) {
        Random aleatoire = new Random();
        Produit = aleatoire.Next(2,6);
    }
}

public class Rose : Plantes {
    public Rose(int[,] position) : base("Rose", position, "Monocarpique", false, new Saisons(3), new Terrains(1), 1, [], [50], 2, 1, new List<Maladies> {}, 1, 3) {}
}

public class Chapeau : Plantes {
    public Chapeau(int[,] position) : base("Chapeau", position, "Monocarpique", true, new Saisons(0), new Terrains(0), 1, [], [100], 24, 2, new List<Maladies> {}, 1, 2) {}
}

public class Nuage : Plantes {
    public Nuage(int[,] position) : base("Nuage", position, "Polycarpique", true, new Saisons(2), new Terrains(0), 1, [], [100], 72, 3, new List<Maladies> {}, 2, 1) {}
}

public class EtoileFilante : Plantes {
    public EtoileFilante(int[,] position) : base("Etoile filante", position, "Monocarpique", false, new Saisons(1), new Terrains(2), 1, [], [50], 2, 1, new List<Maladies> {}, 1, 2) {}
}

public class Alcootier : Plantes {
    public Alcootier(int[,] position) : base("Alcootier", position, "Polycarpique", true, new Saisons(3), new Terrains(3), 4, [], [500], 240, 1, new List<Maladies> {}, 8, 5) {}
}

public class PlanteOrgueilleuse : Plantes {
    public PlanteOrgueilleuse(int[,] position) : base("Plante orgueilleuse", position, "Polycarpique", false, new Saisons(0), new Terrains(4), 1, [], [100], 36, 1, new List<Maladies> {}, 0.75, 3) {}
}

public class Couronne : Plantes {
    public Couronne(int[,] position) : base("Couronne", position, "Monocarpique", true, new Saisons(0), new Terrains(5), 1, [], [200], 12, 1, new List<Maladies> {}, 3, 2) {}
}

public class Planete : Plantes {
    public Planete(int[,] position) : base("Planète", position, "Monocarpique", false, new Saisons(2), new Terrains(6), 1, [], [1000], 60, 1, new List<Maladies> {}, 24, 3) {}
}

public class Lampadaire : Plantes {
    public Lampadaire(int[,] position) : base("Lampadaire", position, "Polycarpique", false, new Saisons(1), new Terrains(7), 1, [], [100], 1200, 0, new List<Maladies> {}, 2, 4) {}
}

public class Baobab : Plantes {
    public List<int> TauxApparition {get; set;}
    public Baobab(int[,] position) : base("Baobab", position, "Monocarpique", false, new Saisons(0), new Terrains(0), 4, [], [700], 240, 0, new List<Maladies> {}, 5, 5) {
        TauxApparition = new List<int> {0, 0, 0, 0, 0, 0, 0};
    }
}

public class Champignon : Plantes {
    public List<int> TauxApparition {get; set;}
    public Champignon(int[,] position) : base("Champignon", position, "Monocarpique", false, new Saisons(0), new Terrains(0), 1, [], [100], 12, 0, new List<Maladies> {}, 1, 1) {
        TauxApparition = new List<int> {0, 0, 0, 0, 0, 0, 0};
    }
}



public class Animaux {
    public string Nom {get; set;}
    public int[,] Position {get; set;}
    public int PlaceOccupée {get; set;}
    public int Groupe {get; set;}
    public List<int> TauxApparition {get; set;}

    public Animaux(string nom, int[,] position, int placeOccupée, int groupe, List<int> tauxApparition) {
        Nom = nom;
        Position = position;
        PlaceOccupée = placeOccupée;
        Groupe = groupe;
        TauxApparition = tauxApparition;
    }
}

public class Serpent : Animaux {
    public Serpent(int[,] position) : base("Serpent", position, 1, 1, new List<int> {0, 0, 0, 0, 0, 0, 0}) {}

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

public class Elephant : Animaux {
    public int[] Dégats {get; set;}
    public Elephant(int[,] position) : base("Éléphant", position, 2, 1, new List<int> {0, 0, 0, 0, 0, 0, 0}) {
        Dégats = [-2000, 0, 0 , 0];
    }

    public void Deplacer() {
        //Si serpent sur une case adjacente, il se déplace à l'opposé
        //Sinon, se déplace en ligne droite (à réfléchir)
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



public class Maladies {
    public string Nom {get; set;}
    public int[] Dégats {get; set;}
    public List<int> TauxApparition {get; set;}
    public int Medicament {get; set;}

    public Maladies(string nom, int[] dégats, List<int> tauxApparition) {
        Nom = nom;
        Dégats = dégats; //nb de dégâts par tour
        TauxApparition = tauxApparition;
        Medicament = 0;
    }
}

public class Mildiou : Maladies {
    public Mildiou() : base("Mildiou", [-8, 0, 0, 0], 2) {}
}

public class Rouille : Maladies {
    public Rouille() : base("Rouille", [-1, 0, 0, 0], 2) {}
}

public class GrandeSoif : Maladies {
    public GrandeSoif() : base("Grande soif", [0, -4, 0, 0], 4) {}
}

public class Isolement : Maladies {
    public Isolement() : base("Isolement", [-5, 0, 0, 0], 15) {}
}

public class PeurNoir : Maladies {
    public PeurNoir() : base("Peur du noir", [-5, 0, 0, 0], 4) {}
}

public class Infertilite : Maladies {
    public Infertilite() : base("Infertilité", [0, 0, 0, 0], 2) {}
}

public class Explosion : Maladies {
    public Explosion() : base("Explosion", [-1, 0, 0, 0], 15) {}
}