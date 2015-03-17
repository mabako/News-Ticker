﻿using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
using ICities;
//using UnityEngine;
using System.Timers;
using ColossalFramework;

namespace News_Ticker
{
    public class ModInfo : IUserMod
    {
        public string Description
        {
            get { return "Show messages from the SimCity 3000 News Ticker"; }
        }

        public string Name
        {
            get { return "SimCity 3000 News Ticker"; }
        }
    }

    public class NewsTicker : ChirperExtensionBase
    {
        private Timer timer = new Timer();
        const int MAX_DISTRICT_COUNT = 128;
        private static System.Random rand = new System.Random();
        string cityName;

        public override void OnCreated(IChirper threading)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "News Ticker Loaded Successfully");

            try
            {
                cityName = Singleton<CityInfoPanel>.instance.GetCityName();
                timer.AutoReset = true;
                timer.Elapsed += new ElapsedEventHandler((sender, e) => AddMessage());
                timer.Interval = 600000;
                timer.Start();
            }
            catch (Exception ex)
            {
                Log.AddError(string.Format("0}: {1}", ex.GetType(), ex.Message));
                endTimer();
            }
        }

        public override void OnReleased()
        {
            endTimer();
            Log.AddMessage("News Ticker Unloaded");
        }

        private void endTimer()
        {
            if (timer != null)
            {
                Log.AddMessage("Stopping News Ticker Timer");
                timer.Stop();
                timer.Dispose();
            }
        }

        private void AddMessage()
        {
            try
            {
                NewsTickerMessage m = new NewsTickerMessage(generateMessage());
                MessageManager.instance.QueueMessage(m);

            }
            catch (Exception ex)
            {
                Log.AddError(string.Format("{0}: {1}", ex.GetType(), ex.Message));
            }
        }

        #region Messages
        private readonly string[] Messages = new string[] {
"Today's Forecast: Cold, Cloudy, With Occasional Showers",
"SimSurvey: 4 Out Of 5 Sims Prefer Hard Cheese To Brie",
"This Space For Rent",
"UFO Seen And Disavowed",
"SimScientist Discovers Abacus Can Be Used To Dry Towels",
"SimSurvey: 80% Of Sims Hang Up On Telephone Solicitors",
"Today's Forecast: Sunny, High 70's, Winds From The East",
"\"Hang Up And Drive\" Say Citizens Against Cell Phones",
"Cat Burglar Spotted, Mistaken For Dalmatian",
"Heads Roll When Rollerblader Hits Tourist Group",
"Typist Involved In Winter Traffic Accident, White-Out Conditions Blamed",
"Sims Flock To Grand Opening Of Betty's Bird Boutique",
"Weasel Rejected As city's Crime-Fighting Mascot",
"SimSurvey: \"Cheese Louise\" Voted Best Pizza Restaurant In city",
"Tapped Out: Local Brewery Closes Its Doors.",
"SimNation Report: Criminals Demand Cell Phones",
"Rap Music Causes Hangnails, Study Shows.",
"Gymnastics Program Growing By Leaps And Bounds",
"Local Scientists Conclude: Kitties Like Fish, Dogs Less Picky",
"Skirmish At Writer's Workshop, Speaker Used Fighting Words",
"Local Politicians Take Both Sides Of Issues, Little Accomplished",
"I Was Framed, Jokes Local Artist",
"city Society Gather To Honor Visiting Potentate, Exchange Kitties",
"Some Destruction, But Not Too Much, In Practical Joke Derby",
"Most Sims Ignore Tickers, Study Reveals",
"Cab Fares In city To Increase; Sims Brace For Worst",
"Broccoli Tops For Moms, Last For Kids; Dads Indifferent",
"Information Shown Here Frequently Absurd, Poll Indicates",
"Eyes Move While Reading Tickers, Scientists Speculate",
"Esoteric Verbosity Culminates In Communicative Ennui, Teachers Note",
"Linguistics Experts Discuss \"Left To Right Or Right To Left; Is One Better?\"",
"Local Mustard Magnate Marries Daughter Of Dill Pickle Mogul",
"Regional Catsup King Cousin To Tie Knot With Toothpick Tycoon",
"Mayor Sees Name In Ticker; Smiles At Irony",
"Studies Show Most Sims Mispronounce \"Zsdersw\"",
"Tip Of The Day Provides Interesting Tidbits, Mayors Agree",
"Local Sim Mentioned In Out Of Town Newspaper; Starts Scrapbook",
"Eckelberry Marmalade May Cure Hiccups, Doctors Say",
"School Field Trip To Museum Sparks Interest In Local History",
"Molasses Truck Springs Leak; Sweetest Accident In Long Time",
"Oliver \"Slim Jim\" Golonsky Wins city Inter-Location Obstacle Race",
"Local Sim Bill Flopsby Heads County Commission On Snuggles And Hugs",
"Cats Demand Longer Breaks, Cleaner Litter, Slower Mice",
"Ordinary Days In city Become Common",
"Weather Likely To Become Different Before Changing",
"Foreign Potentate Becomes Lost In city, Refuses To Ask For Directions",
"Cross-Eyed Python Found To Be Running Successful Chain Of All-Night Laundromats",
"After 36 Years Of Marriage, Man Discovers Wife Is Actually A Rare Yucca Plant",
"Nutritionists Aver That Eating Broccoli Encourages Higher Bowling Scores",
"city Baker Wins Pickled Crumpet Toss Three Years Running",
"Mysterious Loud Rumbling Noises In city Found To Be Mysterious Loud Rumblings",
"Shopping After Hours Source Of Purchase Embarrassment Says Survey",
"Man Caught Shoplifting Spatulas; Thousands Of Flippers Found In Bedroom",
"Pot-Bellied Pigs Named Bob Convention Highlight Of Season",
"Swamp Gas Verified To Be Exhalations Of Stars--Movie Stars--Long Passed",
"Local Cop Found To Be Ticketing Only Lantern-Jawed Males",
"Former High School Principal Caught Licking Stamps Behind Post Office Counter",
"Staring At Football-Shaped Bladders Good For Concentration, Researchers Proclaim",
"Middle Age A Hoax, Declares Study; Turns Out To Be Bad Posture After All",
"Tainted Broccoli Weapon Of Choice For Global Assassins",
"Ditzy Debutante Mistakes Broccoli Floret For Nosegay",
"Cauliflower-Lovers Won't Let Broccoli-Eaters March In Their Neighborhood",
"Never Feed Broccoli To Your Dog, No Matter How Much He Begs",
"Chefs Find Broccoli Effective Tool For Cutting Cheese",
"For More Information, Send 9 Million Simoleons To The \"Broccoli Education Foundation\"",
"Broccoli Found To Cause Grumpiness In Children",
"Talking Broccoli Hosts Talk Show; Guests A \"Bunch Of Vegetables\"",
"Broccoli Discovered To Be Colonies Of Tiny Aliens With Murder On Their Minds",
"Original Magna Carta Found Written On Large Broccoli Stalk",
"Miracle Lint Remover Based On Broccoli Juice Sweeps Market",
"Broccoli Pops Cereal Not As Popular As Presumed",
"House Made Entirely Of Broccoli Built In city; Furniture Made Of Wheat Germ",
"Timmy Falls Down Well, Climbing Needs Work",
"SimScientist Discovers New Dry Cleaning Method Using Sparklers",
"SimSurvey: 80% Of Sims Love Clog-Dancing",
"Today's Forecast: Windy And Cooler Than Yesterday",
"Cable Disruption Blamed For Rising Birthrates",
"Scientists Assert That Swearing Is Source Of Bad Breath",
"Four In Five city Children Won't Eat Mono-Colored Cereals",
"Ancient Meteorite Revealed To Be Burnt Burger",
"Giant Hairball Has Perfect Grammar, Linguists Say",
"Man Survives Wintry Night Adhered To Bus Bench By Chewing Gum",
"Study Demonstrates That Singing In The Shower Makes Teeth Crooked",
"Cat Hijacks Municipal Bus; Riders Applaud Good Timing At Stops And Courteous Meows",
"Ham-Handedness Doesn't Lead To Higher Cholesterol, Researchers Declare",
"Ball Lightning Destroys Toupee But Polishes Victim's Car",
"Mediums Agree Blue-Striped Socks No More Lucky Than Clovers Or Pennies",
"cityPhonebooks Print All Wrong Numbers; Results In 15 New Marriages",
"Staring At Lapping Ocean Waves Makes You More Assertive During Lunch",
"Semicolon Declared Sexier Than Comma At Grammarian's Fete",
"\"Weasels Are Warm And Wonderful\" Day At city Mall",
"Humming Show Tunes Sure Sign Of Poor Motor Skills, Researchers Declare",
"State Governor Found To Be Mule; \"His Clothes Always Fit Funny,\" Says Aide",
"Sim Offers To Let City Bus Run Him Over For Lifetime Salad Bar Privileges",
"Unsalted Tortilla Chips Best Cure For Colds Says Health Nut",
"Man Discovers Neighbor Completely Enclosed In Mailbox; Returns Him For Postage",
"All Raccoons Cheat At Poker, Animal Researchers Say",
"50 Car Pile-Up Results In New City Sculpture",
"Building Turned Into Aviary After Birds Stick To New Paint",
"Daily Special At Restaurant Found To Be Big Fat Lie",
"Girl Rides Bicycle Across City Phone Wires; Arrested For Eavesdropping",
"Floor Sweepings Found To Be Tangier Than Salt And Pepper",
"Survey Shows Less Is More, More Or Less",
"Ten Teachers With Cardiac Arrest After Students Declare Love Of Beowulf",
"French Kissing Leads To Higher Croissant Use, Authorities Warn",
"Bongos Making Big Comeback Among Unemployed Steelworkers",
"Lying Found To Be Effective Calorie Reducer",
"Lou Turns Away Every Person Who Skips To Her; \"They Have No Rhythm,\" She Says",
"Gravy Tastes Better When Loudly Slurped; Scientists Baffled",
"Tree Stuck In Cat; Firefighters Baffled",
"SimSurvey: Sims Sleep Seven Hours",
"SimSurvey: 4 Out Of 5 Sims Surveyed Find Surveys Satisfactory",
"SimSurvey: 50% Of Sims Say YES",
"SimSurvey Reports Rise in Vegetarian Sims",
"Stand Up And Cheer If You Like SimCity",
"Sim Scientist Discovers Gravity While Falling Down Stairs",
"Boy Saves Cat From Tree, Thousands Cheer",
"Public Service Message: Pooper Scoopers Urged When Walking Dogs",
"SimSurvey: 3 Out Of 5 Sims Loathe Modern Art",
"Mrs. SimLeary Gets Prize Cow",
"Fresh Fruit In Season Is Berry, Berry Good",
"Lady's Knitting Circle Raises Cash For Homeless",
"Black And White Ball Preparations Underway",
"Black And White Ball Disrupted By Bank Robbery",
"Black And White Ball Raises Money For Charity",
"\"I'm Just A Sim, Sim, Simple Guy\" Rises To Top Of Charts",
"SimNation To Host Energy Symposium",
"Citywide Blood Drive Highlights SimHealth Week",
"Sims Report Widespread SimAnt Problem",
"Bark Art Exhibition By Bark Simson",
"Bus Misses Turn, Dozens Late For Work",
"city Makes Top 10 List",
"Experts Advise Using Sunblock As Sunny Weather Continues",
"Here Comes The Sun",
"Lunar Eclipse Obscured By Clouds",
"Psychic K.C. Edgars Predicts City To Grow",
"city Racewalkers Win All-City Title",
"city Baton Twirlers To Lead Local Parade",
"Marathon! Sims Hit The Ground Running",
"Big Game Bistro Opens Amid Animal Rights Protests",
"Newspaper Boy Crime Ring Cracked: Read All Over",
"SimFirefighters Wanted: Apply At Your Local Fire Station",
"SimPolice Officers Wanted: Apply At Your Local Police Precinct",
"Bookstore Gets New Copies Of SimUlations: A Love Story",
"Truckload Of Apples Overturns, city Diner Offers Applesauce Special",
"Crime Lord Spotted In city; Mayor Says \"No Comment\"",
"city Tourist Bureau Launches City Beautification Project",
"Don't Forget To Pick Up Your Litter",
"Consider A Career In Garbage Collection",
"Pigeon Alert! Extreme Pigeon Danger!",
"Spotted Owl Spotted",
"Public Displays Of Affection Common Sight Near City Hall",
"Eagerly Awaited Llama Exhibition Coming Soon",
"Sims Everywhere Agree: Vote Early, Vote Often",
"Sims Everywhere Agree: Frequent Saving Prevents File Loss",
"Sims Everywhere Agree: Purring Kitties Are Happy Kitties",
"Sims Everywhere Agree: Good Grooming Is Essential To Success",
"Sims Everywhere Agree: Brush Before, After, And Between Meals",
"Sims Everywhere Agree: The Egg Came First, But Only After The Chicken",
"Sims Everywhere Agree: If You Throw A Stone, It Will Hit Something",
"Sims Everywhere Agree: It's Not What You Make, It's What You Keep",
"Sims Everywhere Agree: A Sound Financial Future Begins With Inheriting Lots Of Money",
"Sims Everywhere Agree: All Sales Are Final",
"Sims Everywhere Agree: Your Actual Costs May Vary",
"Sims Everywhere Agree: Past Performance Does Not Guarantee Future Returns",
"Sims Everywhere Agree: History Laughs At Many People Who Deserved To Be Laughed At",
"Sims Everywhere Agree: For The Best In News Ticker Entertainment, The Picayune Can't Be Beat",
"Tommy B. Saif Sez: Look Both Ways Before Crossing The Street",
"Tommy B. Saif Sez: Stay Within The Crosswalk",
"Tommy B. Saif Sez: Hold On; Sudden Stops Sometimes Necessary",
"Tommy B. Saif Sez: Keep Fingers Away From Moving Panels",
"Tommy B. Saif Sez: No Left Turn, Except Buses",
"Tommy B. Saif Sez: Return Seats And Trays To Their Proper Upright Position",
"Tommy B. Saif Sez: Eating And Drinking On Station Platforms Is Prohibited",
"Tommy B. Saif Sez: Accept No Substitutes, And Don't Be Fooled By Imitations",
"Tommy B. Saif Sez: Do Not Remove This Tag Under Penalty Of Law",
"Tommy B. Saif Sez: Always Mix Thoroughly When So Instructed",
"Tommy B. Saif Sez: Try To Keep Six Month's Expenses In Reserve",
"Tommy B. Saif Sez: Change Not Given Without Purchase",
"Tommy B. Saif Sez: If You Break It, You Buy It",
"Tommy B. Saif Sez: Reservations Must Be Cancelled 48 Hours Prior To Event To Obtain Refund",
"From The Desk Of Wise Guy Sammy: One Word In This Ticker Is Mispelled",
"From The Desk Of Wise Guy Sammy: One Word In This Ticker Is Wrong",
"From The Desk Of Wise Guy Sammy: One Word In This Ticker Is Sdrawkcab (Backwards)",
"From The Desk Of Wise Guy Sammy: Lightning Often Strikes The Same Place More Than Once",
"From The Desk Of Wise Guy Sammy: Better To Aim High And Miss Then To Aim Low And Hit",
"From The Desk Of Wise Guy Sammy: Reading Improves The Mind And Lifts The Spirit",
"From The Desk Of Wise Guy Sammy: Two Points Determine A Straight Line",
"From The Desk Of Wise Guy Sammy: Help Is The Sunny Side Of Control",
"From The Desk Of Wise Guy Sammy: A Person Who Likes Cats Can't Be All Bad",
"From The Desk Of Wise Guy Sammy: It's Better To Yield Right Of Way Than To Demand It",
"From The Desk Of Wise Guy Sammy: You Can't Outwait A Bureaucracy",
"From The Desk Of Wise Guy Sammy: Check Your Facts Before Making A Fool Of Yourself",
"From The Desk Of Wise Guy Sammy: It Is Easier To Get Forgiveness Than Permission",
"From The Desk Of Wise Guy Sammy: If You Made The Mess, You Clean It Up",
"From The Desk Of Wise Guy Sammy: You Don't Have To Fool All The People, Just The Right Ones",
"From The Desk Of Wise Guy Sammy: Wine And Friendships Get Better With Age",
"From The Desk Of Wise Guy Sammy: It's Hard To Have Too Much Shelf Space",
"From The Desk Of Wise Guy Sammy: The Insides Of Golf Balls Are Mostly Rubber Bands",
"Pistol Packing Punks Pilfer Precious Petunias",
"Traveling Truck Technician Talks Transmissions Tuesday",
"Bread Baking Books Beat Bean Broiling",
"city News Ticker: Journalistic Integrity Without All The Advertising",
"city News Ticker: No Advertisements Since Before The Beginning",
"city News Ticker: Pretty Darn Accurate Most Of The Time",
"city News Ticker: Information That's A Luxury, Not A Necessity",
"city News Ticker: For When You Have To Know But Would Rather Not",
"city News Ticker: Easier To Find Because It Moves",
"city News Ticker: Left To Right Through Aesthetic Design",
"city News Ticker: Information With As Few Words As Possible",
"city News Ticker: Sometimes We're Just Here To Make You Smile",
"city News Ticker: Don't Blame Us If You're Not Paying Attention",
"city News Ticker: Don't Blame Us, We Just Report It",
"city News Ticker: If It's Important To You, It Probably Is To Us Too",
"city News Ticker: Properly Spelled Words From Beginning To End",
"city News Ticker: A Quiet Voice Of Reason In A Noisy World",
"city News Ticker: Not For The Faint Of Heart",
"city News Ticker: Information At A Readable Speed",
"city News Ticker: Not Too Fast, Not Too Slow, Just Right",
"city News Ticker: If You Read It Here, That Means It Happened",
"city News Ticker: Just One Piece Of Information After Another",
"city News Ticker: Accept No Substitutes",
"city News Ticker: Your Total Information Source",
"city New Ticker: Important Things You Need To Know, More Or Less",
"Rumor Of Kitty Kibble Shortage Causes Futures To Drop; Consumers Stockpile",
"Local Merchants Puzzled By Rumors Of Kitty Kibble Shortage; \"We Have Plenty,\" Say Most",
"Kitties Concerned By Rumors Of Kitty Kibble Shortage; Owners In Panic",
"No Kitty Kibble Shortage Says Local Representative Of Kitty Kibble Association",
"Is Kitty Kibble Shortage Real? Authorities Say Rumors Unfounded",
"Unlicensed Kitty Kibble Factory Fuels Rumors Of Shortage",
"Enough Kitty Kibble For Twenty Years, Experts Agree",
"Kitty Kibble Association Flooded With Letters: Where Is Kitty Kibble?",
"Is Kitty Kibble Shortage A Hoax? Sims Search For Truth",
"Rumors Persist In Kitty Kibble Shortage; Unfounded Say Many",
"Kitties Want Answers In Possible Kitty Kibble Shortage",
"Kitties Say Not Enough Being Done In Kitty Kibble Shortage",
"No Kitty Kibble Shortage, Officials Insist; Kitties Skeptical",
"city News Ticker: Where We Report On Busses, Not Buses",
"Famed Prognosticator Warns \"Disaster Awaits Us All\"",
"Doughnuts: Is There Anything They Can't Do?",
"Rock Star Spotted In Llama Fur Near Casa Del Sticky",
"9 Out Of 10 Sims Prefer Cranberry Jelly Over Preserves",
"Cure For Senility Found, But Lost Before Being Recorded",
"Rockin' Good Thrash Metal Found To Reverse Aging Process",
"Local Kindergartners Prefer Oi Brand Paste; Claim It Just Tastes Better",
"Local Sim Discovers Just Who Wrote Book Of Love",
"If Tin Whistles Are Made Of Tin, What Do They Make Foghorns Out Of?",
        };
        #endregion

        private string generateMessage()
        {
            string message = getRandomMessage();

            return message.Replace("city", cityName);
        }

        private string getRandomMessage()
        {
            return Messages[rand.Next(Messages.Length + 1)];
        }

    }

    public class NewsTickerMessage : MessageBase
    {
        private string m_text;
        private string m_author = "News Ticker";
        public NewsTickerMessage(string message)
        {
            m_text = message;
        }

        public override string GetText()
        {
            return m_text;
        }

        public override string GetSenderName()
        {
            return m_author;
        }

        public override bool IsSimilarMessage(MessageBase other)
        {
            NewsTickerMessage newsTickerMessage = other as NewsTickerMessage;

            return newsTickerMessage != null && this.m_text == newsTickerMessage.m_text;
        }

        public override void Serialize(ColossalFramework.IO.DataSerializer s)
        {
            s.WriteSharedString(m_text);

        }

        public override void Deserialize(ColossalFramework.IO.DataSerializer s)
        {
            m_text = s.ReadSharedString();
        }
    }

    public static class Log
    {
        public static void AddMessage(string message)
        {
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, message);
        }

        public static void AddError(string error)
        {
            error = "[News Ticker] " + error;
            DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Error, error);
        }
    }
}
