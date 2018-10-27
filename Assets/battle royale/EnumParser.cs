using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumParser : MonoBehaviour
{
    static public string[] boysNames = { "Liam", "Noah", "William", "James", "Logan", "Benjamin", "Mason", "Elijah", "Oliver", "Jacob", "Lucas", "Michael", "Alexander", "Ethan", "Daniel", "Matthew", "Aiden", "Henry", "Joseph", "Jackson", "Samuel", "Sebastian", "David", "Carter", "Wyatt", "Jayden", "John", "Owen", "Dylan", "Luke", "Gabriel", "Anthony", "Isaac", "Grayson", "Jack", "Julian", "Levi", "Christopher", "Joshua", "Andrew", "Lincoln", "Mateo", "Ryan", "Jaxon", "Nathan", "Aaron", "Isaiah", "Thomas", "Charles", "Caleb", "Josiah", "Christian", "Hunter", "Eli", "Jonathan", "Connor", "Landon", "Adrian", "Asher", "Cameron", "Leo", "Theodore", "Jeremiah", "Hudson", "Robert", "Easton", "Nolan", "Nicholas", "Ezra", "Colton", "Angel", "Brayden", "Jordan", "Dominic", "Austin", "Ian", "Adam", "Elias", "Jaxson", "Greyson", "Jose", "Ezekiel", "Carson", "Evan", "Maverick", "Bryson", "Jace", "Cooper", "Xavier", "Parker", "Roman", "Jason", "Santiago", "Chase", "Sawyer", "Gavin", "Leonardo", "Kayden", "Ayden", "Jameson", "Kevin", "Bentley", "Zachary", "Everett", "Axel", "Tyler", "Micah", "Vincent", "Weston", "Miles", "Wesley", "Nathaniel", "Harrison", "Brandon", "Cole", "Declan", "Luis", "Braxton", "Damian", "Silas", "Tristan", "Ryder", "Bennett", "George", "Emmett", "Justin", "Kai", "Max", "Diego", "Luca", "Ryker", "Carlos", "Maxwell", "Kingston", "Ivan", "Maddox", "Juan", "Ashton", "Jayce", "Rowan", "Kaiden", "Giovanni", "Eric", "Jesus", "Calvin", "Abel", "King", "Camden", "Amir", "Blake", "Alex", "Brody", "Malachi", "Emmanuel", "Jonah", "Beau", "Jude", "Antonio", "Alan", "Elliott", "Elliot", "Waylon", "Xander", "Timothy", "Victor", "Bryce", "Finn", "Brantley", "Edward", "Abraham", "Patrick", "Grant", "Karter", "Hayden", "Richard", "Miguel", "Joel", "Gael", "Tucker", "Rhett", "Avery", "Steven", "Graham", "Kaleb", "Jasper", "Jesse", "Matteo", "Dean", "Zayden", "Preston", "August", "Oscar", "Jeremy", "Alejandro", "Marcus", "Dawson", "Lorenzo", "Messiah", "Zion", "Maximus" };
    static public string[] girlsNames = { "Emma", "Olivia", "Ava", "Isabella", "Sophia", "Mia", "Charlotte", "Amelia", "Evelyn", "Abigail", "Harper", "Emily", "Elizabeth", "Avery", "Sofia", "Ella", "Madison", "Scarlett", "Victoria", "Aria", "Grace", "Chloe", "Camila", "Penelope", "Riley", "Layla", "Lillian", "Nora", "Zoey", "Mila", "Aubrey", "Hannah", "Lily", "Addison", "Eleanor", "Natalie", "Luna", "Savannah", "Brooklyn", "Leah", "Zoe", "Stella", "Hazel", "Ellie", "Paisley", "Audrey", "Skylar", "Violet", "Claire", "Bella", "Aurora", "Lucy", "Anna", "Samantha", "Caroline", "Genesis", "Aaliyah", "Kennedy", "Kinsley", "Allison", "Maya", "Sarah", "Madelyn", "Adeline", "Alexa", "Ariana", "Elena", "Gabriella", "Naomi", "Alice", "Sadie", "Hailey", "Eva", "Emilia", "Autumn", "Quinn", "Nevaeh", "Piper", "Ruby", "Serenity", "Willow", "Everly", "Cora", "Kaylee", "Lydia", "Aubree", "Arianna", "Eliana", "Peyton", "Melanie", "Gianna", "Isabelle", "Julia", "Valentina", "Nova", "Clara", "Vivian", "Reagan", "Mackenzie", "Madeline", "Brielle", "Delilah", "Isla", "Rylee", "Katherine", "Sophie", "Josephine", "Ivy", "Liliana", "Jade", "Maria", "Taylor", "Hadley", "Kylie", "Emery", "Adalynn", "Natalia", "Annabelle", "Faith", "Alexandra", "Ximena", "Ashley", "Brianna", "Raelynn", "Bailey", "Mary", "Athena", "Andrea", "Leilani", "Jasmine", "Lyla", "Margaret", "Alyssa", "Adalyn", "Arya", "Norah", "Khloe", "Kayla", "Eden", "Eliza", "Rose", "Ariel", "Melody", "Alexis", "Isabel", "Sydney", "Juliana", "Lauren", "Iris", "Emerson", "London", "Morgan", "Lilly", "Charlie", "Aliyah", "Valeria", "Arabella", "Sara", "Finley", "Trinity", "Ryleigh", "Jordyn", "Jocelyn", "Kimberly", "Esther", "Molly", "Valerie", "Cecilia", "Anastasia", "Daisy", "Reese", "Laila", "Mya", "Amy", "Teagan", "Amaya", "Elise", "Harmony", "Paige", "Adaline", "Fiona", "Alaina", "Nicole", "Genevieve", "Lucia", "Alina", "Mckenzie", "Callie", "Payton", "Eloise", "Brooke", "Londyn", "Mariah", "Julianna", "Rachel", "Daniela", "Gracie", "Catherine", "Angelina", "Presley" };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    static public string HumanName(int nameID,bool isBoy)
    {
        return isBoy ? boysNames[nameID]: girlsNames[nameID];
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
