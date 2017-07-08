using System;

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

    public static class OptionExtensions
    {
        public static Option<TContentOut> Map<TContentIn, TContentOut>(this Option<TContentIn> option, Func<TContentIn, TContentOut> map)
        {
            switch (option)
            {
                case Some<TContentIn> some:
                    return new Some<TContentOut>(map(some.Content));
                default:
                    return new None<TContentOut>();
            }
        }

        public static Option<TContentOut> FlatMap<TContentIn, TContentOut>(this Option<TContentIn> opt,
            Func<TContentIn, Option<TContentOut>> flatMap)
        {
            switch (opt)
            {
                case Some<TContentIn> some:
                    return flatMap(some.Content);
                default:
                    return new None<TContentOut>();
            }
        }

        public static TContent GetOrDefault<TContent>(this Option<TContent> option, Func<TContent> defaultFunc)
        {
            switch (option)
            {
                case Some<TContent> some:
                    return some.Content;
                default:
                    return defaultFunc();
            }
        } 
    }
}
