using UnityEngine;

public interface ICollector
{
    void Visit(Coin coin);

    void Visit(FirstAidKit kit);
}
