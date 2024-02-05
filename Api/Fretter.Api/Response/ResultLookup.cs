namespace Fretter.Api.Response
{
    public class ResultLookup<TIdentyity>
    {
        public ResultLookup()
        {

        }
        public ResultLookup(TIdentyity id, object text)
        {
            this.id = id;
            this.label = text;
        }
        public TIdentyity id { get; set; }
        public string value => this.id.ToString();
        public object label { get; set; }
    }
}
