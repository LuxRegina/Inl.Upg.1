namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary = new List<SweEngGloss>();  
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }

                                                                    // ToDo - Add Input method

        static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");    // ToDo - add write help for commands and load which file to work with
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();     
                string command = argument[0];
                if (command == "quit")                              // ToDo - add 'Help' command
                {
                    Console.WriteLine("Goodbye!");                  
                }
                else if (command == "load")
                {
                    if(argument.Length == 2)
                    {
                        using (StreamReader sr = new StreamReader(argument[1]))   // ToDo - sr - new name  and Catch if file isnt found
                        {
                            dictionary = new List<SweEngGloss>(); // Prevents duplication if multiple loadouts are made!
                            string line = sr.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = sr.ReadLine();
                            }
                        }
                    }
                    else if(argument.Length == 1)
                    {
                        using (StreamReader sr = new StreamReader(defaultFile))     // ToDo - sr - new name  and Catch if file isnt found
                        {
                            dictionary = new List<SweEngGloss>(); // Prevents duplication if multiple loadouts are made!
                            string line = sr.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = sr.ReadLine();
                            }
                        } // Close file?
                    }
                }
                else if (command == "list")                                         // NYI - Try-Catch if trying to list before loading a file
                {
                    foreach(SweEngGloss gloss in dictionary)
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                    }
                }
                else if (command == "new")
                {
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));  // ToDo - CW state swedish word first
                    }
                    else if(argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string s = Console.ReadLine();                          // ToDo - new variable for s
                        Console.Write("Write word in English: ");
                        string e = Console.ReadLine();                          // ToDo - new variable for e
                        dictionary.Add(new SweEngGloss(s, e));
                    }
                }
                else if (command == "delete")
                {
                    if (argument.Length == 3)
                    {
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++) {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                                index = i;
                        }
                        dictionary.RemoveAt(index);                            // NYI Catch if word requested isnt in the file
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string s = Console.ReadLine();                      // ToDo - new variable for s
                        Console.Write("Write word in English: ");
                        string e = Console.ReadLine();                      // ToDo - new variable for e
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == s && gloss.word_eng == e)
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        foreach(SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == argument[1])
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == argument[1])
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }                                                                                   // NYI - Catch if word(s) requested isnt in file
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string s = Console.ReadLine();                          // ToDo - new variable for s
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == s)
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == s)
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }                                                                                   // NYI - Catch if word(s) requested isnt in file
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }
                    // NYI - Add methods/functions for all arguments
    }
}