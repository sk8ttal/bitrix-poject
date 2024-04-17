namespace atFrameWork2.TestEntities
{
    public class Form
    {
        public Form(string title)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
        }

        public string Title { get; set; }
    }
}
