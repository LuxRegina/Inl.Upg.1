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
            Console.WriteLine("Welcome to the dictionary app!\n Write 'help' for a list of commands.");    // ToDo - add write help for commands and load which file to work with
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();     
                string command = argument[0];
                if (command == "help")                            
                {
                    Console.WriteLine("Available commands:\n" +
                        "'load'-------- load in the file you want to work with.\n" +
                        "'list'-------- list all words in the dictionary.\n" +
                        "'new'--------- add word to dictionary.\n" +
                        "'translate'--- translate a word.\n" +
                        "'delete'------ delete a word in the dictionary.\n" +
                        "'quit'-------- ends the program.");                  
                }
                else if (command == "load")
                {
                    if(argument.Length == 2)
                    {
                        using (StreamReader textfile = new StreamReader(argument[1]))   // ToDo - Catch if file isnt found
                        {
                            dictionary = new List<SweEngGloss>(); // Prevents duplication if multiple loadouts are made!
                            string line = textfile.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = textfile.ReadLine();
                            }
                        }
                    }
                    else if(argument.Length == 1)
                    {
                        using (StreamReader textfile = new StreamReader(defaultFile))     // ToDo - Catch if file isnt found
                        {
                            dictionary = new List<SweEngGloss>(); // Prevents duplication if multiple loadouts are made!
                            string line = textfile.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = textfile.ReadLine();
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
                        string sweWord = Console.ReadLine();                          
                        Console.Write("Write word in English: ");
                        string engWord = Console.ReadLine();                          
                        dictionary.Add(new SweEngGloss(sweWord, engWord));
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
                        string sweWord = Console.ReadLine();                      
                        Console.Write("Write word in English: ");
                        string engWord = Console.ReadLine();                      
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == sweWord && gloss.word_eng == engWord)
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
                        string transWord = Console.ReadLine();                          
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == transWord)
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == transWord)
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }                                                                                   // NYI - Catch if word(s) requested isnt in file
                    }
                }
                else if (command == "quit")                              
                {
                    Console.WriteLine("Goodbye!");
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