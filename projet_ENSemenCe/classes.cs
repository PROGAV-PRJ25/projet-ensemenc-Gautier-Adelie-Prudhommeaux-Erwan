public class Terrains{
    public string TypeSol {get;set;}
    public string Nomplanete {get;set;}
    public List<Maladies>? MaladiesPossible {get; set;}
    public List<Plantes>? PlanteAchetable {get;set;}
    public Météos? MeteoAjouté {get;set;} 


    public Terrains(int numplanete, List<Maladies> maladiesPossible, List<Plantes> planteAchetable, Météos meteoAjouté){
        T = ["Aucun","Petit Prince","Businessman","Buveur","Vaniteux","Roi","Géographe","Réverbère"];
        Nomplanete = T[numplanete];
        MaladiesPossible = maladiesPossible;
        PlanteAchetable = planteAchetable;
        MeteoAjouté = meteoAjouté;
    }

}
public class TerrainPetitPrince{
    public TerrainPetitPrince():base(1,new list<Maladies> {},new list<plantes> {},new Meteos(0)){}
}
public class TerrainBusinessman{
    public TerrainBusinessman():base(2,new list<Maladies> {},new list<plantes> {},new Meteos(1)){}
}
public class TerrainBuveur{
    public TerrainBuveur():base(3,new list<Maladies> {},new list<plantes> {},new Meteos(3)){}
}
public class TerrainRoi{
    public TerrainRoi():base(4,new list<Maladies> {},new list<plantes> {},new Meteos(4)){}
}
public class TerrainGéographe{
    public TerrainGéographe():base(5,new list<Maladies> {},new list<plantes> {},new Meteos(5)){}
}
public class TerrainRéverbère{
    public TerrainRéverbère():base(6,new list<Maladies> {},new list<plantes> {},new Meteos(6)){}
}

