using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PokemonDatabase {
    private static PokemonData[] pokedex = new PokemonData[] {
        null,
        new PokemonData(1, "Bulbasaur", PokemonData.Type.GRASS, PokemonData.Type.POISON, "Overgrow", null, "Chlorophyll", 87.5f, 45, 0.7f, 6.9f, 64, 0, 0, 0, 1, 0, 0, 70, "Seed",
                "For some time after its birth, it grows by gaining nourishment from the seed on its back.", 45, 49, 49, 65, 65, 45,
                new int[] {1, 3, 7, 13, 13, 15, 19, 21, 25, 27, 31, 33, 37},
                new string[] {"Tackle", "Growl", "Leech Seed", "Vine Whip", "Poison Powder", "Sleep Powder", "Take Down", "Razor Leaf", "Sweet Scent", "Growth", "Double-Edge",
                    "Worry Seed", "Synthetis", "Seed Bomb"},
                new string[] {},
                new int[] {2},
                new string[] {"Level, 16"}),
            new PokemonData(4, "Charmander", PokemonData.Type.FIRE, PokemonData.Type.NONE, "Blaze", null, "Solar Power", 87.5f, 45, 2.0f, 18.7f, 65, 0, 0, 0, 0, 0, 1, 70, "Lizard",
                "The flame on its tail indicates Charmander’s life force. If it is healthy, the flame burns brightly.", 39, 52, 43, 60, 50, 65,
                new int[] {1, 1, 7, 10, 16, 19, 25, 28, 34, 37, 43, 46},
                new string[] {"Growl", "Scratch", "Ember", "Smokescreen", "Dragon Rage", "Sleep Powder", "Scary Face", "Fire Fang", "Flame Burst", "Slash", "Flamethrower",
                    "Fire Spin", "Inferno"},
                new string[] {},
                new int[] {2},
                new string[] {"Level, 36"})
    };

    public static PokemonData getPokemon(int ID) {
        PokemonData result = null;
        int i = 1;

        while (result == null || i < pokedex.Length) {
            if (pokedex[i].getID() == ID) {
                result = pokedex[i];
                return result;
            }
            i += 1;
//            if (i >= pokedex.Length + 1) {
//                return null;
//            }
        }
        return result;
    }

    public static int getLevelExp(int current) {
        if (current > 100) {
            current = 100;
        }

        return expTable[current - 1];
    }

    private static int[] expTable = new int[] {
        0, 8, 27, 64, 125, 216, 343, 512, 729, 1000,
        1331, 1728, 2197, 2744, 3375, 4096, 4913, 5832, 6859, 8000,
        9261, 10648, 12167, 13824, 15625, 17576, 19683, 21952, 24389, 27000,
        29791, 32768, 35937, 39304, 42875, 46656, 50653, 54872, 59319, 64000,
        68921, 74088, 79507, 85184, 91125, 97336, 103823, 110592, 117649, 125000,
        132651, 140608, 148877, 157464, 166375, 175616, 185193, 195112, 205379, 216000,
        226981, 238328, 250047, 262144, 274625, 287496, 300763, 314432, 328509, 343000,
        357911, 373248, 389017, 405224, 421875, 438976, 456533, 474552, 493039, 512000,
        531441, 551368, 571787, 592704, 614125, 636056, 658503, 681472, 704969, 729000,
        753571, 778688, 804357, 830584, 857375, 884736, 912673, 941192, 970299, 1000000
    };
}
