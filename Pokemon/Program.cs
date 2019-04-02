using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class Program
    {
        static void Main(string[] args)
        {
            // Make a Pokemon list 
            List<Pokemon> roster = new List<Pokemon>();

            // Make three new lists with fire,water and grass moves 
            List<Move> fireMoves = new List<Move>();
            List<Move> waterMoves = new List<Move>();
            List<Move> grassMoves = new List<Move>();
            
            //Add fire moves to the fire list 
            fireMoves.Add(new Move("Ember"));
            fireMoves.Add(new Move("Fire Blast"));

            //Add water moves to the water list 
            waterMoves.Add(new Move("Bubble"));
            waterMoves.Add(new Move("Bite"));

            //Add grass moves to the grass list 
            grassMoves.Add(new Move("Cut"));
            grassMoves.Add(new Move("Mega Drain"));
            grassMoves.Add(new Move("Razor Leaf"));



            // INITIALIZE YOUR THREE POKEMONS HERE
            // Add three new Pokemons to the list 
            roster.Add(new Pokemon("Charmander", 3, 52, 43, 39, Elements.Fire, fireMoves));
            roster.Add(new Pokemon("Squirtle", 2, 48, 65, 44, Elements.Water, waterMoves));
            roster.Add(new Pokemon("Bulbasaur", 3, 49, 49, 45, Elements.Grass, grassMoves));

            Console.WriteLine("Welcome to the world of Pokemon!\nThe available commands are list/fight/heal/quit");

            while (true)
            {
                Console.WriteLine("\nPlese enter one of the abovementioned commands in order to continue");
                switch (Console.ReadLine())
                {
                    
                    case "list":
                        // PRINT THE POKEMONS IN THE ROSTER HERE
                        Console.WriteLine("These Pokemons are available:");
                        // Show all available pokemons from the roster 
                        foreach (Pokemon p in roster)
                        {
                            Console.WriteLine(p.Name);
                        }
                        break;

                    case "fight":
                        //PRINT INSTRUCTIONS AND POSSIBLE POKEMONS (SEE SLIDES FOR EXAMPLE OF EXECUTION)
                        Console.Write("Choose who should fight:");
                        Console.WriteLine("(Example: Bulbasaur Squirtle)");

                        //READ INPUT, REMEMBER IT SHOULD BE TWO POKEMON NAMES
                        // Make a string called pokemonNames
                        string pokemonNames = Console.ReadLine();

                        // Make an array which splits the two names 
                        string[] vs = pokemonNames.Split(' ');

                        Pokemon player = null;
                        Pokemon enemy = null;

                        //BE SURE TO CHECK THE POKEMON NAMES THE USER WROTE ARE VALID (IN THE ROSTER) AND IF THEY ARE IN FACT 2!
                        // Go thorugh all the Pokemon in order to chose who to fight with
                        // By going through the foreach it already checks if Pokemon names are valid
                        foreach (Pokemon p in roster)
                        {
                            
                            // Checks if the length of the array is shorter or longer than 3 to make sure that the user chooses 2 pokemons
                            if (vs.Length == 1 || vs.Length == 3) 
                            {
                                Console.WriteLine("You chose a wrong number of pokemons, try again");
                                //go back to case "fight" 
                                goto case "fight";
                            }

                            // The first input in the array is your pokemon
                            if (p.Name == vs[0])
                            {
                                Console.WriteLine("You have chosen" + " " + p.Name);
                                player = p;
                            }

                            // The second input in the array is your enemy
                            if (p.Name == vs[1])
                            {
                                Console.WriteLine("Your enemy is" + " " + p.Name);
                                enemy = p;
                              
                            }
                            
                            
                            // If the pokemon you chose is the same as your opponent, then you go to case fight again
                            if (vs[0] == vs[1])
                            {
                                Console.WriteLine("You can't chose" + " " + vs[0] + " " + "as your Pokemon and your opponent");
                                Console.WriteLine("Try again");
                                goto case "fight";
                            }
                            
                           
                        }
                       

                        //if everything is fine and we have 2 pokemons let's make them fight
                        if (player != null && enemy != null && player != enemy)
                        {
                            Console.WriteLine("A wild " + enemy.Name + " appears!");
                            Console.Write(player.Name + " I choose you! ");

                            //BEGIN FIGHT LOOP
                            while (player.Hp > 0 && enemy.Hp > 0)
                            {
                                //PRINT POSSIBLE MOVES
                                Console.Write("What move should we use? (");
                                
                                foreach (Pokemon p in roster)
                                {
                                    // Check if the name of a pokemon is the same as the players pokemon, then go thorugh the possible moves
                                    if(player.Name == p.Name)
                                    {
                                        // Go through each move of the pokemon you chose and give you the index(IndexOf(m)) of the move 
                                        foreach (Move m in p.Moves)
                                        {
                                            Console.WriteLine(p.Moves.IndexOf(m) + " " + m.Name);
                                        }
                                    }
                                }

                                

                                //GET USER ANSWER, BE SURE TO CHECK IF IT'S A VALID MOVE, OTHERWISE ASK AGAIN
                                // Press the number and then parse the string to an int
                                string inputMove = Console.ReadLine();
                                int pokemonMoves = int.Parse(inputMove);  

                                // If the number you press is >= 0 and it is smaller than the amount of moves it prints the choosen move
                                if (pokemonMoves >= 0 && pokemonMoves < player.Moves.Count)
                                {
                                    Console.WriteLine("You chose to use" + " " + player.Moves[pokemonMoves].Name);
                                }
                                else  
                                {
                                    Console.WriteLine("Invalid move, chose again");
                                    // It starts the while loop over
                                    continue;

                                }
                                
                                    

                                //CALCULATE AND APPLY DAMAGE
                                int damage;
                                // The damage is player.Attack and it attack the enemy 
                                damage = player.Attack(enemy);

                                //print the move and damage
                                Console.WriteLine(player.Name + " uses " + player.Moves[pokemonMoves].Name + ". " + enemy.Name + " loses " + damage + " HP");

                                //if the enemy is not dead yet, it attacks
                                if (enemy.Hp > 0)
                                {
                                    //CHOOSE A RANDOM MOVE BETWEEN THE ENEMY MOVES AND USE IT TO ATTACK THE PLAYER
                                    Random rand = new Random();
                                    //Placeholders 
                                    int enemyMove = -1;
                                    int enemyDamage = -1;

                                    // enemyDamage is the attack og the enemy (enemy.Attack) and it attacks the player 
                                    enemyDamage = enemy.Attack(player);
                                    // checks if the enemy is Charmander because then it only have two attacks 
                                    if(enemy.Name == "Charmander")
                                    {
                                        // it chooses randomly between 2 moves 
                                        enemyMove = rand.Next(2);
                                    }
                                    // checks if the enemy is Squirtle because then it only have two attacks 
                                    if (enemy.Name == "Squirtle")
                                    {
                                        // it chooses randomly between 2 moves 
                                        enemyMove = rand.Next(2);
                                    }
                                    // checks if the enemy is Bulbasaur because then it only have two attacks 
                                    if (enemy.Name == "Bulbasaur")
                                    {
                                        // it chooses randomly between 3 moves 
                                        enemyMove = rand.Next(3);
                                    }

                                    //print the move and damage
                                    Console.WriteLine(enemy.Name + " uses " + enemy.Moves[enemyMove].Name + ". " + player.Name + " loses " + enemyDamage + " HP");
                                }
                            }
                            //The loop is over, so either we won or lost
                            if (enemy.Hp <= 0)
                            {
                                Console.WriteLine(enemy.Name + " faints, you won!");
                            }
                            else
                            {
                                Console.WriteLine(player.Name + " faints, you lost...");
                            }
                        }

                        //otherwise let's print an error message
                        else
                        {
                            Console.WriteLine("Invalid pokemon");
                        }
                        break;

                    case "heal":
                        //RESTORE ALL POKEMONS IN THE ROSTER
                        foreach(Pokemon p in roster)
                        {
                            p.Restore();
                        }
                        Console.WriteLine("All pokemons have been healed");
                        break;

                    case "quit":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            }
        }
    }
}
