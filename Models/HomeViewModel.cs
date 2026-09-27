namespace EgeHavaleProjeOdevi.Models
{
    public class HomeViewModel
    {
        public List<Ev> Evler { get; set; } = new();
        public List<Vasita> Vasitalar { get; set; } = new();
        public List<Esya> Esyalar { get; set; } = new();
        public List<IsIlani> IsIlanlari { get; set; } = new();
    }
    public class SearchViewModel
    {
        public string Query { get; set; }
        public List<Ev> Evler { get; set; } = new();
        public List<Vasita> Vasitalar { get; set; } = new();
        public List<Esya> Esyalar { get; set; } = new();
        public List<IsIlani> IsIlanlari { get; set; } = new();
        public int TotalCount => Evler.Count + Vasitalar.Count + Esyalar.Count + IsIlanlari.Count;
    }
}