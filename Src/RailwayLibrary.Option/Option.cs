namespace RailwayLibrary.Option
{
    public abstract class Option<TContent>
    {
        public static Option<TContent> Some(TContent content) => new Some<TContent>(content);

        public static Option<TContent> None() => new None<TContent>();
    }
    
    public class Some<TContent> : Option<TContent>
    {
        public Some(TContent content)
        {
            Content = content;
        }

        public TContent Content { get; }
    }

    public class None<TContent> : Option<TContent> { }

    public static class Option
    {
        public static Option<TContent> Some<TContent>(TContent content) => new Some<TContent>(content);

        public static Option<TContent> None<TContent>() => new None<TContent>();

        public static bool TryRetrieve<TContent>(this Option<TContent> option, out TContent content)
        {
            if (option is Some<TContent> some)
            {
                content = some.Content;
                return true;
            }

            content = default(TContent);
            return false;
        }
    }
}
