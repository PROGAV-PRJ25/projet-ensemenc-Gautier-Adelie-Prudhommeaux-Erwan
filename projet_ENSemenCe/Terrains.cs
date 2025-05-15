//------------------------------------ Terrains -------------------------------------

public class Terrains{
    public string Nomplanete {get;set;}
    public Maladies? MaladiesPossible {get; set;}
    public List<Plantes>? PlanteAchetable {get;set;}
    public Meteos? MeteoAjouté {get;set;} 


    public Terrains(int numplanete, Maladies maladiesPossible, List<Plantes> planteAchetable, Meteos meteoAjouté){
        string[] nomTerrains = ["Aucun","Petit Prince","Businessman","Buveur","Vaniteux","Roi","Géographe","Réverbère"];
        Nomplanete = nomTerrains[numplanete];
        MaladiesPossible = maladiesPossible;
        PlanteAchetable = planteAchetable;
        MeteoAjouté = meteoAjouté;
    }
}

public class TerrainPetitPrince:Terrains{
    public TerrainPetitPrince():base(1,new Maladies {},new List<Plantes> {},new TempêteStellaire()){}
}
public class TerrainBusinessman:Terrains{
    public TerrainBusinessman():base(2,new List<Maladies> {},new List<Plantes> {},new CriseEconomique()){}
}
public class TerrainBuveur:Terrains{
    public TerrainBuveur():base(3,new List<Maladies> {},new List<Plantes> {},new FortePluie()){}
}
public class TerrainRoi:Terrains{
    public TerrainRoi():base(4,new List<Maladies> {},new List<Plantes> {},new Obligation()){}
}
public class TerrainGéographe:Terrains{
    public TerrainGéographe():base(5,new List<Maladies> {},new List<Plantes> {},new ChangementSaison()){}
}
public class TerrainRéverbère:Terrains{
    public TerrainRéverbère():base(6,new List<Maladies> {},new List<Plantes> {},new ToutNoir()){}
}


//------------------------------------ Météos -------------------------------------

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


//------------------------------------ Catastrophes -------------------------------------

public class Secheresse : Meteos{
    public Secheresse():base("Secheresse",1){
    }
}
public class Gel : Meteos {
    public Gel():base("Gel",2){
    }
}
public class TempêteStellaire : Meteos {
    public TempêteStellaire():base("Tempête Stellaire",3){
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