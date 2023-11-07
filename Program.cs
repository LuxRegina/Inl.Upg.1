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
        static string Input(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
        static void Main(string[] args)
        {
            string defaultFile ="dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!\n Write 'help' for a list of commands.");
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];

                if (command == "help")
                {
                    Help();
                }
                else if (command == "load")
                {
                    if (argument.Length == 2)
                    {
                        string lastFileLoaded = argument[1];
                        Load(lastFileLoaded);                    
                    }
                    else if (argument.Length == 1)
                    {
                        string lastFileLoaded = defaultFile;
                        Load(lastFileLoaded);                        
                    }
                }
                else if (command == "list")                                         // NYI - Try-Catch if trying to list before loading a file
                {
                    ListAll();
                }
                else if (command == "new")
                {
                    if (argument.Length == 3)
                    {
                        Console.WriteLine("Make sure you stated the swedish word before the english word.");
                        string sweWord = argument[1];
                        string engWord = argument[2];
                        New(sweWord, engWord);
                    }
                    else if (argument.Length == 1)
                    {
                        string sweWord = Input("Write word in Swedish: ");
                        string engWord = Input("Write word in English: ");

                        New(sweWord, engWord);          
                    }
                }
                else if (command == "delete")
                {
                    if (argument.Length == 3)
                    {
                        string sweWord = argument[1];
                        string engWord = argument[2];

                        Delete(sweWord, engWord);
                    }
                    else if (argument.Length == 1)
                    {
                        string sweWord = Input("Write word in Swedish: ");
                        string engWord = Input("Write word in English: ");

                        Delete(sweWord, engWord);
                    }
                    else if (argument.Length == 2)
                    { Console.WriteLine("Please type in both translations of the word to delete it."); }
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        string transWord = argument[1];
                        Translate(transWord);                                                                                             
                    }
                    else if (argument.Length == 1)
                    {
                        string transWord = Input("Write what word to translate: ");
                        Translate(transWord);                                                 
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
        private static void Help()            
        {
            Console.WriteLine("Available commands:\n" +
                        "'load'-------- load in the file you want to work with.\n" +
                        "'list'-------- list all words in the dictionary.\n" +
                        "'new'--------- add word to dictionary.\n" +
                        "'translate'--- translate a word.\n" +
                        "'delete'------ delete a word in the dictionary.\n" +
                        "'quit'-------- ends the program.");
        }
        private static void Load(string lastFileLoaded)
        {
            try
            {
                using (StreamReader textfile = new StreamReader(lastFileLoaded))
                {
                    dictionary = new List<SweEngGloss>(); // Prevents duplication if multiple loadouts are made!
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                    string line = textfile.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                    while (line != null)
                    {
                        SweEngGloss gloss = new SweEngGloss(line);
                        dictionary.Add(gloss);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        line = textfile.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                    }
                }
            } catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("File could not be found, did you type it in correctly?");
            }

        }
        private static void ListAll()
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
            }
        }
        private static void Translate(string transWord)
        {
            bool found = false;
            foreach (SweEngGloss gloss in dictionary)
            {
                if (gloss.word_swe == transWord)
                { 
                    found = true; 
                    Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                }
                if (gloss.word_eng == transWord)
                {
                    found = true;
                    Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                }
            }
            if (!found) Console.WriteLine("Word not in dictionary.");
        }
        private static void New(string sweWord, string engWord)
        {
            dictionary.Add(new SweEngGloss(sweWord, engWord));
        }
        private static void Delete(string sweWord, string engWord)
        {
            try
            {
                int index = -1;
                for (int i = 0; i < dictionary.Count; i++)
                {
                    SweEngGloss gloss = dictionary[i];
                    if (gloss.word_swe == sweWord && gloss.word_eng == engWord)
                        index = i;
                }
                dictionary.RemoveAt(index);                             
            }
           catch (System.ArgumentOutOfRangeException) {

                Console.WriteLine("Word doesnt exist in dictionary.");
            }

        }
    }
}