using System;
using System.Collections.Generic;

namespace CollectionsAssignments
{
    public struct CrewMember
    {
        public string Name;
        public int CountTask;
    }

    public struct CargoItem
    {
        public ulong Key;
        public double Mass;
        public double Volume;
    }

    public struct CargoAction
    {
        public long SampleType;
        public int Amount;
    }

    public class CargoItem2
    {
        public long Code { get; set; }
        public int Amount { get; set; }
        public string LoadDate { get; set; }
        public string ShipName { get; set; }
        public string Keeper { get; set; }

        public CargoItem2(long code, int amount, string loadDate, string shipName, string keeper)
        {
            Code = code;
            Amount = amount;
            LoadDate = loadDate;
            ShipName = shipName;
            Keeper = keeper;
        }
    }

    public class Command
    {
        public string Action { get; set; }
        //UNLOAD OR LOAD értékeket fogad, tehát lehetne enum is
        public int SampleType { get; set; }
        public int Amount { get; set; }

        public Command(string action, int sampleType, int amount)
        {
            Action = action;
            SampleType = sampleType;
            Amount = amount;
        }

    }

    public class Package
    {
        public string Type;
        public int SIze;

        public Package(string type, int size)
        {
            Type = type;
            SIze = size;
        }
    }


    public class MainProgram
    {
        public static void Main(string[] args)
        {
            //1 Első feladat: Legproduktívabb űrhajósok rangsorolása
            Random random = new Random();
            List<CrewMember> crewMembers = new List<CrewMember>();

            for(int i = 0; i<25;i++)
            {
                CrewMember crewMember = new CrewMember();
                crewMember.Name = "Crew Member " + (i + 1);
                crewMember.CountTask = random.Next(1, 100); // Random task count between 1 and 99
                // Add to the sorted dictionary with a unique key
                crewMembers.Add(crewMember);
            }

            SortedDictionary<int, List<string>> ranking = new SortedDictionary<int, List<string>>(
            Comparer<int>.Create((x, y) => y.CompareTo(x)));


            foreach (var member in crewMembers)
            {
                if (!ranking.ContainsKey(member.CountTask))
                {
                    ranking[member.CountTask] = new List<string>();
                }
                ranking[member.CountTask].Add(member.Name);
            }

            int topCount = 5;
            int j = 0;
            Console.WriteLine("Top " + topCount + " most productive crew members");
            // az öt első legproduktívabb tag kiírását kell megvalósítani

            foreach (var entry in ranking)
            {
                foreach (var name in entry.Value)
                {
                    Console.WriteLine(name + " - " + entry.Key + " tasks");
                    j++;
                    if (j >= topCount) break;
                }
                if (j >= topCount) break;
            }

            //2 Második feladat: CargoItems
            List<CargoItem> cargoItems = new List<CargoItem>();
            HashSet<ulong> keys = new HashSet<ulong>();

            //random data generation
            for(int i = 0; i<200; i++)
            {
                ulong k = (ulong)random.Next(1, 200);
                double m = (double)random.Next(1, 1000);
                double v = (double)random.Next(1, 1000);
                CargoItem cargoItem = new CargoItem();
                cargoItem.Key = k;
                cargoItem.Mass = m;
                cargoItem.Volume = v;
                cargoItems.Add(cargoItem);
                keys.Add(k);
            }
            Console.WriteLine("\nCargo Items: 200");
            Console.WriteLine("Keys: " + keys.Count);

            //3 Dictionary
            Dictionary<string, int> cargoHold = new Dictionary<string, int>();
            cargoHold.Add("TypeA", 50);
            cargoHold.Add("TypeB", 30);
            cargoHold.Add("TypeC", 20);
            Console.WriteLine("Enter a typename:");
            string typeName = Console.ReadLine();
            Console.WriteLine("Available quantity of " + typeName + ": " +
                (cargoHold.ContainsKey(typeName) ? cargoHold[typeName].ToString() : "0"));
            Console.WriteLine("Enter a typename to remove:");
            string removeTypeName = Console.ReadLine();
            if (cargoHold.ContainsKey(removeTypeName))
            {
                cargoHold.Remove(removeTypeName);
                Console.WriteLine(removeTypeName + " removed from cargo hold.");
            }
            else
            {
                Console.WriteLine(removeTypeName + " not found in cargo hold.");
            }
            Console.WriteLine("Random cargo hold contents:");
            List<string> keyList = new List<string>(cargoHold.Keys);
            Random rand = new Random();
            string randomKey = keyList[rand.Next(keyList.Count)];
            Console.WriteLine("Randomly selected cargo type: " + randomKey +
                " with quantity: " + cargoHold[randomKey]);
            cargoHold.Remove(randomKey);
            Console.WriteLine($"Remaining sample types in cargo hold: {cargoHold.Count}");

            //4 feladat Actions
            Stack<CargoAction> actions = new Stack<CargoAction>();
            actions.Push(new CargoAction { SampleType = 1, Amount = 10 });
            actions.Push(new CargoAction { SampleType = 2, Amount = 20 });
            actions.Push(new CargoAction { SampleType = 3, Amount = 30 });
            Console.WriteLine("Undo the last operation? (Y/N)");
            string undoChoice = Console.ReadLine();
            if (undoChoice.ToUpper() == "Y")
            {
                if (actions.Count > 0)
                {
                    CargoAction lastAction = actions.Pop();
                    Console.WriteLine("Last action undone: SampleType " + lastAction.SampleType + ", Amount " + lastAction.Amount);
                }
                else
                {
                    Console.WriteLine("No actions to undo.");
                }
            }
            else
            {
                Console.WriteLine("No actions undone.");
            }

            //5 feladat: LinkedList
            //CargoItem már volt, de most CargoItem2-t használjuk
            CargoItem2 cargoItem2 = new CargoItem2(123456789, 100, "2023-10-01", "Starship", "John Doe");
            CargoItem2 cargoItem2_2 = new CargoItem2(987654321, 200, "2023-10-02", "Galaxy Cruiser", "Jane Smith");
            CargoItem2 cargoItem2_3 = new CargoItem2(456789123, 150, "2023-10-03", "Nebula Explorer", "Alice Johnson");
            LinkedList<CargoItem2> wareHouse = new LinkedList<CargoItem2>();
            wareHouse.AddLast(cargoItem2);
            wareHouse.AddLast(cargoItem2_2);
            wareHouse.AddLast(cargoItem2_3);
            foreach (var item in wareHouse)
            {
                Console.WriteLine($"CargoItem2 - Code: {item.Code}, Amount: {item.Amount}, LoadDate: {item.LoadDate}, ShipName: {item.ShipName}, Keeper: {item.Keeper}");
            }
            wareHouse.Remove(cargoItem2);
            foreach (var item in wareHouse)
            {
                Console.WriteLine($"CargoItem2 - Code: {item.Code}, Amount: {item.Amount}, LoadDate: {item.LoadDate}, ShipName: {item.ShipName}, Keeper: {item.Keeper}");
            }

            //6 feladat: Queue
            Queue<Command> commandQueue = new Queue<Command>();
            Stack<Command> executedCommands = new Stack<Command>();
            commandQueue.Enqueue(new Command("Load", 1, 10));
            commandQueue.Enqueue(new Command("Unload", 2, 5));
            commandQueue.Enqueue(new Command("Load", 3, 15));
            while(commandQueue.Count > 0)
            {
                Command command = commandQueue.Dequeue();
                executedCommands.Push(command);
                Console.WriteLine($"Executed Command: Action={command.Action}, SampleType={command.SampleType}, Amount={command.Amount}");
                Console.WriteLine("Do you want to undo the last command? (Y/N)");
                string undoCommandChoice = Console.ReadLine();
                if (undoCommandChoice.ToUpper() == "Y")
                {
                    Console.WriteLine("How many commands do you want to undo?");
                    int undoCount = int.Parse(Console.ReadLine());
                    for (int i = 0; i < undoCount && executedCommands.Count > 0; i++)
                    {
                        Command lastCommand = executedCommands.Pop();
                        commandQueue.Enqueue(lastCommand); // Re-enqueue the command to the queue
                        Console.WriteLine($"Undone Command: Action={lastCommand.Action}, SampleType={lastCommand.SampleType}, Amount={lastCommand.Amount}");
                    }
                }
            }
            Console.WriteLine("All commands executed and undone as per user input.");

            //7 feladat: List
            List<Package> packages = new List<Package>();
            packages.Add(new Package("Box", 10));
            packages.Add(new Package("Envelope", 5));
            packages.Add(new Package("Crate", 20));

            foreach (var package in packages)
            {
                Console.WriteLine($"Package Type: {package.Type}, Size: {package.SIze}");
            }

            Console.WriteLine("Enter a packagesize");
            int packageSize = int.Parse(Console.ReadLine());

            foreach (var package in packages)
            {
                if (package.SIze == packageSize)
                {
                    Console.WriteLine($"Found package: Type={package.Type}, Size={package.SIze}");
                    break;
                }
            }
        }
    }
}
