using Unity.VisualScripting;
using UnityEngine;
using static Factory;

public class Builder : MonoBehaviour
{
    public class Item
    {
        public string name;
        public float damage;
        public bool isMagical;
        public bool isRare;

        public class Builder
        {
            public Item item;

            public Builder(string name)
            {
                item.name = name;
            }
            public Builder WithDamage(float damage)
            {
                item.damage = damage;
                return this;
            }
            public Builder MakeMagical()
            {
                item.isMagical = true;
                return this;
            }
            public Builder MakeRare()
            {
                item.isRare = true;
                return this;
            }

            public Item Build()
            {
                return item;
            }
        }
    }
    public class Pet
    {
        public string name;
        public float maxHealth;
        public float currentHealth;
        public int age;
        public string race;
        public bool hasBalls;
        public bool hasCancer;

        public class Builder
        {
            public Pet pet;

            public Builder(string name, float maxHealth, int age, string race)
            {
                pet.name = name;
                pet.maxHealth = maxHealth;
                pet.age = age;
                pet.race = race;
            }

            public Builder MaxStartingHealth()
            {
                pet.currentHealth = pet.maxHealth;
                return this;
            }

            public Builder CustomStartingHealth(float health)
            {
                pet.currentHealth = health;
                return this;
            }
            public Builder PutBalls()
            {
                pet.hasBalls = true;
                return this;
            }
            public Builder PutCancer()
            {
                pet.hasCancer = true;
                return this;
            }

            public Pet Build()
            {
                return pet;
            }
        }
    }

    public enum ItemType
    {
        Sword,
        Staff,
        Axe
    }
    public class ItemFactory
    {
        public Item Create(ItemType type)
        {
            switch (type)
            {
                case ItemType.Sword:
                    return new Item.Builder("Sword")
                        .WithDamage(100)
                        .Build();
                case ItemType.Staff:
                    return new Item.Builder("Staff")
                        .WithDamage(40)
                        .MakeMagical()
                        .MakeRare()
                        .Build();
                case ItemType.Axe:
                    return new Item.Builder("Axe")
                        .WithDamage(300)
                        .MakeRare()
                        .Build();
                default:
                    return null;
            }
        }
    }
    public enum PetType
    {
        Dog,
        Cat,
        Slave,
        Bird
    }

    public class PetFactory
    {
        public Pet Create(PetType type)
        {
            switch (type)
            {
                case PetType.Dog:
                    return new Pet.Builder("Firulais", 100, 7, "Golden Retreiver")
                        .MaxStartingHealth()
                        .PutCancer()
                        .Build();
                case PetType.Cat:
                    return new Pet.Builder("Michi", 50, 9, "Naranja")
                        .MaxStartingHealth()
                        .PutCancer()
                        .Build();
                case PetType.Slave:
                    return new Pet.Builder("No Importa", 250, 45, "Negro")
                        .CustomStartingHealth(100)
                        .PutBalls()
                        .Build();
                case PetType.Bird:
                    return new Pet.Builder("Mordecai", 20, 4, "Azulejo Azul")
                        .MaxStartingHealth()
                        .PutBalls()
                        .Build();
                default:
                    return null;
            }
        }
    }
}
