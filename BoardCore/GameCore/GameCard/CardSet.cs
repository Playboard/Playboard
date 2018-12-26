using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BoardCore.GameCore.GameCard
{
    /// <summary>
    /// A `<typeparamref name="T"/>` Based <see cref="LinkedList{Card}"/> store in <see cref="Dictionary{T, LinkedList}"/>
    /// </summary>
    /// <typeparam name="T">key type</typeparam>
    public class CardSet<T> : ICardHolder<T>
    {
        private readonly Dictionary<T, LinkedList<Card>> AllCards = new Dictionary<T, LinkedList<Card>>();

        public CardSet()
        {
        }

        private LinkedList<Card> GetCardList(T Key)
        {
            if (!AllCards.ContainsKey(Key))
            {
                AllCards.Add(Key, new LinkedList<Card>());
            }
            return AllCards[Key];
        }

        public void AddCard(T Key, Card Card)
        {
            GetCardList(Key).AddLast(Card);
        }

        public bool HasCard(T Key)
        {
            return AllCards.ContainsKey(Key) && GetCardList(Key).Count > 0;
        }

        public Card PickCard(T Key)
        {
            var list = GetCardList(Key);
            if (list.Count > 0)
            {
                var ret = list.Last.Value;
                list.RemoveLast();
                return ret;
            }
            else
            {
                return null;
            }
        }

        public Card<G, R> PickAs<G, R>(T Key)
            where G : Game<G>
            where R : Card<G, R>
        {
            return PickCard(Key).ToCard<G, R>();
        }

        public Card PickRandomCard()
        {
            var rand = (new Random()).Next(AllCards.Count);
            var list = AllCards.Values.Skip(rand).FirstOrDefault(l => l.Count > 0);
            if (list == null) return null;
            var ret = list.Last();
            list.RemoveLast();
            return ret;
        }

        public Card<G, R> PickRandomCard<G, R>()
            where G : Game<G>
            where R : Card<G, R>
        {
            return PickRandomCard() as Card<G, R>;
        }
    }
}
