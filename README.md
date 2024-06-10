# DemNoir
### Demented Noir Work Through Checklist
- [x] write to JSON file the node position
- [x] finish the CheckChildValidity Function
	- this means a node is neither null, nor is it locked by a stat restriction
- [x]  set node position and stats from JSON file on startup
	- [x] Lookup how to read from a JSON File 
	- [x] It resets the position but not the choice data for some reason so I need to fix that
- [x] have stat saving be set by clicking an option, not the stat button
- [x] Have stat popup from clicking stat button
- [x] have settings popup
	- [x] have actual settings for something
		- probably a reset game function and sound controls
- [x] Fix stat changes and answers so they're associated with each other as opposed to node page
	- [x] probably make an Answer Class or Struct
	- [x] Have the text actually represent the stats - They're not updating in real time
		- [x] Need to update TaskOnClick For all to not break due to inappropriate Prev Usage
	- [x] Add set stats functionality aka "Mind =  5"
	- [x] (optional) do the same with stat restrictions for extremely accurate choice restrictions
- [x] Branching choices aren't functioning properly. Buttons that do have nodes that exist as the lead aren't actually doing anything
	- [x] Alright, I'm not making new nodes based off choice node Ids which is what's fucking it up.
	- [x] Okay by making the DefNext and prev sequential (aka connecting all the nodes into a linked list) I'm able to access all the nodes. 
- [x] Add functionality for switching to a scene with images in it, and for images to be used - For some reason its not switching scenessss
	- [x] image switching based off text
- [x] Main Menu
- [x] Make the game run in a certain size - (I made it be able to change size)
- [x] Make the game function after building
- [x] Make sure all the anchors are good

- -- Far Off Stuff
- [x] Music
- [X] better art
- [ ] uploading the whole demo project's writing from Kelvin
- [ ] Add an inventory system
- [x] make text scrolling for large text blocks - (I made it autosize)
- [x] implement removal of image block if there is no image to be used

Choose your own adventure application. UI based with Node data structure. LinkedList/Graph Nodes - info found [[LinkedList Nodes |here]] 

- Each Node has an
	- Id - value to search for if need be.
	- StatRestrict - determines if this particular node should be stat restricted and by what 
	- StatChange - determines if this particular node has any stat changes associated with it.
	- Question - The question to be displayed for the player
	- Title - Title of the page
	- ImageTitle - Title of the image to be displayed on the page if there is one
	- C1s . . . C4s - the string for each choice on the page. If "Null" appears blank
	- Prev - previous node, allows for backtracking
	- Defnext - the default next, not needed for gameplay, but useful for testing and branch creation
	- C1 . . . C4 - the nodes for the various choice options

The Stat format can be found at [[Demented Noir Stats]] 
>[!example]- The format for the data input is
> [int] 1 Node ID
> [string] 2 Question
> [string] 3 Title
> [string] 4 ImageTitle
> [string] 5 Answer 0
> [string] 6 Answer 1
> [string] 7 Answer 2
> [string] 8 Answer 3
> [string] 9 Answer 4
> [int] 10 Previous node
> [int] 11 Default next node
> [int] 12 Answer 0 Node ID - must be sequential. Even if DefNext doesn't make sense gamewise, its necessary for it to create all the nodes and actual branching paths.
> [string] 13 Answer 0 Stat Changes 
> 	- Format: stat operator int 
> 	- Ex: Mind + 2
> [string] 14 Answer 0 Stat Restrictions
> 	- Format: stat operator int 
> 	- Ex: Mind + 15 
> [int] 15 Answer 1 Node ID
> [string] 16 Answer 1 Stat Changes
> [string] 17 Answer 1 Stat Restrictions
> [int] 18 Answer 2 Node ID
> [string] 19 Answer 2 Stat Changes
> [string] 20 Answer 2 Stat Restrictions
> [int] 21 Answer 3 Node ID
> [string] 22 Answer 3 Stat Changes
> [string] 23 Answer 3 Stat Restrictions
> [int] 24 Answer 4 Node ID
> [string] 25 Answer 4 Stat Changes
> [string] 26 Answer 4 Stat Restrictions

Repeat Immediately the next sequential node.


What I attempted to add to Nodes are
 - Stat Restricted Choices
 - Stat Increasing/Decreasing Choices
 
>[!faq]- How to go about this for stat changes:
> Add a string variable named StatChange to each node, if empty or "Null" it is not active, if it has for example "Heart + 3" or "Mind - 2" increase or decrease the respective stat based on the third value. This can be done after a node is selected. So if given multiple choices for stat increases, they obviously only increase after choosing one of the nodes

>[!faq]- How to go about this for Stat Restricted Choices:
> Similar to above, add a string variable named StatRestrict to each node, if empty or "Null" it is not active, if it has " Heart + 15" or "Mind - 25" then the choice must be restricted and voided out if they either have respectively less than 15 hearts, or more than 25 mind. This way it can be restricted if too focused on a choice as well. The value at the beginning of the string is to point at which choice is the focus. So if there are multiple options that are restricted, there can be multiple invalid. The information obviously has to be on the node providing the choices, not on the nodes with each choice. Although I suppose I could have it look at each choice and determine if they have a stat restriction. Hmm that might be a better option. Perhaps when the page is updated check the child nodes once for stat restrictions, and apply a Void function if the restrictions aren't met. I think that's better than the previous method but I'll keep both ideas here for now.



I figured out how to do save data, which is a step in the right direction for the above options. 
I'll list the code below to write to a JSON file, as well as a boilerplate version in [[Unity Tips and Tricks]] I had to create an empty gameObject to connect this script to, then on the stats button I added an onClick option, placed the gameObject as the focus object, and selected the outputJSON as the function to do. 

```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditorInternal;

public class JSONWrite : MonoBehaviour
{

    [System.Serializable]
    public class playerStats
    {
        public int nodeid;
        public int mind;
        public int heart;
        public int sneakiness;
        public int strength;
    }


    public playerStats myPlayer = new playerStats();

    public void outputJSON() 
    {
        string strOutput = JsonUtility.ToJson(myPlayer);

        File.WriteAllText(Application.dataPath + "/resources/text.txt", strOutput);


    }
    
}

```


