using System;
using System.Collections.Generic;
using System.Text;

namespace BoardCore.GameCore.GameCard
{
    public interface ICardHolder<T>
    {
        Card PickCard(T Key);
        void AddCard(T Key, Card Card);
        bool HasCard(T Key);
    }
}
