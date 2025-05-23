//------------------------------------ Terrains -------------------------------------

public abstract class Terrains{
    public string Nomplanete {get;set;}
    public string? MaladiesPossible {get; set;}
    public List<string>? PlanteAchetable {get;set;}
    public double[] MeteoPourcentage {get;set;} 


    public Terrains(int numplanete, string maladiesPossible, List<string> planteAchetable, double[] meteoAjoute){
        string[] nomTerrains = ["Aucun","Petit Prince","Businessman","Buveur","Vaniteux","Roi","Géographe","Réverbère"];
        Nomplanete = nomTerrains[numplanete];
        MaladiesPossible = maladiesPossible;
        PlanteAchetable = planteAchetable;
        MeteoPourcentage = meteoAjoute;
    }
}

public class TerrainPetitPrince:Terrains{
    public TerrainPetitPrince():base(1,"Mildiou" ,new List<string> {"Rose"},[0,0,0,6]){}
}
public class TerrainBusinessman:Terrains{
    public TerrainBusinessman():base(2,"Infertilité" ,new List<string> {"Etoile filante"},[1,1,0.8,7]){}
}
public class TerrainBuveur:Terrains{
    public TerrainBuveur():base(3,"Grande soif" ,new List<string> {"Alcootier"},[1,1,0.7,8]){}
}
public class TerrainVaniteux:Terrains{
    public TerrainVaniteux():base(4,"Isolement" ,new List<string> {"Plante orgueilleuse"},[1,1,0.7,9]){}
}
public class TerrainRoi:Terrains{
    public TerrainRoi():base(5,"Rouille" ,new List<string> {"Couronne"},[1,1,0.7,10]){}
}
public class TerrainGeographe:Terrains{
    public TerrainGeographe():base(6,"Explosion" ,new List<string> {"Planète"},[1,1,0.7,11]){}
}
public class TerrainRéverbère:Terrains{
    public TerrainRéverbère():base(7,"Peur du noir" ,new List<string> {"Lampadaire"},[1,1,0.7,12]){}
}


//------------------------------------ Météos -------------------------------------

public class Meteos{
    public string Nom {get;set;}
    public int NumeroCata {get;set;} // 0 pas de catastrrphe sinon numéro de la catastrophes a definir pour chaque
    public Meteos(string nom, int cata){
        Nom = nom;
        NumeroCata = cata;
    }
    public override string ToString()
    {

        return Nom;
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


//------------------------------------ Catastrophes -------------------------------------

public class Secheresse : Meteos{
    public Secheresse():base("Secheresse",1){
    }
}
public class Gel : Meteos {
    public Gel():base("Gel",2){
    }
}
public class TempeteStellaire : Meteos {
    public TempeteStellaire():base("Tempête Stellaire",3){
    }
}
public class CriseEconomique : Meteos {
    public CriseEconomique():base("Crise économique",4){
    }
}
public class FortePluie : Meteos {
    public FortePluie():base("Forte Pluie",5){
    }
}
public class HordeAnimaux : Meteos {
    public HordeAnimaux():base("Horde d’Animaux",6){
    }
}
public class Obligation : Meteos {
    public Obligation():base("Obligation",7){
    }
}
public class ChangementSaison : Meteos {
    public ChangementSaison():base("Changement de saison",8){
    }
}
public class ToutNoir : Meteos {
    public ToutNoir():base("Tout Noir",9){
    }
}


//------------------------------------ Saisons -------------------------------------

public class Saisons {
    public string Nom {get;set;}
    public double[] PourcentMeteos {get;set;}
    public Saisons(string nom, double[] pourcentMeteos){
        Nom = nom;
        PourcentMeteos = pourcentMeteos;
    }
}

public class Saison1 : Saisons{
    public Saison1():base("Saison1",[20,20,20,20,8,5,1,0,0,0,0,0,0]){}//[20,20,20,20,8,5,1,1,1,1,1,1,1]
}
public class Saison2 : Saisons{
    public Saison2():base("Saison2",[20,20,20,20,5,8,1,0,0,0,0,0,0]){}//[20,20,20,20,5,8,1,1,1,1,1,1,1]
}
public class Saison3 : Saisons{
    public Saison3():base("Saison3",[50,15,15,15,0,0,0.8,0,0,0,0,0,0]){}//[50,15,15,15,0,0,0.8,0.7,0.7,0.7,0.7,0.7,0.7]
}