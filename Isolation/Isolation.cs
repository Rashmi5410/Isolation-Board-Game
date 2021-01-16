using System;
using static System.Console;

namespace Bme121
{
    static class Program
    {
    
        static bool [,] board;
        static int rows, columns;//number of rows and columns requested  
        static int platformRowA,platformColumnA;//platform position for A
        static int platformRowB, platformColumnB;//platform position for B
        static int positionRowA, positionColumnA;//position for pawn A
        static int positionRowB, positionColumnB;//position for pawn B
        static bool gameRunning = true;//checks to see if players can keep playing 
        static int moveRowA, moveColumnA;//move player A wants to make
        static int moveRowB, moveColumnB;//move player B wants to make 
        static string nameA, nameB; 
        static int removableRowA, removableColumnA;//tile player A wants to remove
        static int removableRowB, removableColumnB;//tile player B wants to remove
        
        
        
        static void Main( )//Call other methods in the main method
        {
            Initialization();
            while(gameRunning)//put in loop until players cannot play anymore
            {
                DrawGameBoard(); 
                Moves();
            }
        }
        
    //Start of Initialization Method 
    
    static void Initialization()
    {
    
        Console.Clear(); 
    
        string[ ] letters = { "a","b","c","d","e","f","g","h","i","j","k","l",
          "m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
          
        int[] numbers = new int [26];
            
            for (int d = 0; d < numbers.Length; d++)
            {
                numbers[ d ] = d;
            }
    
        Write( "Enter your name [default Player A]: ");//prompt first player for name
        nameA = ReadLine();
        if (nameA.Length ==0) nameA= "Player A";//default name is Player A if nothing is entered 
        WriteLine( "name: {0}", nameA); 
    
        Write( "Enter your name [default Player B]: ");//prompt second player for name
        nameB = ReadLine();
        if (nameB.Length ==0) nameB= "Player B";//default name is Player B if nothing is entered 
        WriteLine( "name: {0}", nameB); 
    
        bool isValid =false; 
   
    
        while (isValid==false) 
        {
            Write("Enter the number of board rows {4-26, default 6}: ");
            string response1 = ReadLine(); 
            if (response1.Length==0) rows=6;//sets default as 6 if player does not enter anything
            else rows = int.Parse (response1); 
            if (rows < 4||rows > 26)//error check to ensure player enters valid number of rows
            {
                WriteLine( "The number of rows entered must be between 4 and 26"); 
            }
            else isValid= true; 
        }
    
        isValid = false;
        while (isValid==false)
        {
            Write("Enter the number of board columns {4-26, default 8}: ");
            string response1 = ReadLine(); 
            if (response1.Length==0) columns=8;//default set as 8 if player does not enter anything 
            else columns = int.Parse (response1);
            if (rows < 4||columns > 26)//error check to ensure player enters valid number of columns
            {
                WriteLine( "The number of columns entered must be between 4 and 26"); 
            }
            else isValid= true; 
        } 
    
        board = new bool [rows, columns]; //sets board to true
        for(int r=0; r<rows; r++)
        {
            for(int z=0; z<columns; z++)
            {
                board[r,z] = true; 
            }
        }

// players platforms 

        bool check = false;
        while( check == false)
        {

            Write( "Enter a letter for Pawn A's Platform Row or Press Enter: ");//prompts user for platform row A (row of starting tile) 
            string response = ReadLine (); 
            platformRowA = Array.IndexOf(letters, response); 
        
            if(response.Length == 0)
            {
                platformRowA = positionRowA = (int) Math.Ceiling((rows-1)/2.0);//automatically sets starting position for pawn A
                WriteLine("The default row is {0}", letters[platformRowA] );
                check=true;
            }
        
            else if(response.Length > 1)//checks length entered
            {
                WriteLine("Error! You did not enter 1 letter.");
            }
        
            else
            {
                if (platformRowA > rows-1)
                {
                    WriteLine( "Error!Enter a valid letter for Pawn A's Platform Row." );//ensures row is on board
                    platformRowA = Array.IndexOf(letters, response);
                }
                else check = true;
            }
        }
        
        check = false; 
        while(check == false)
        {
    
            Write( "Enter a letter for Pawn A's Platform Columns or Press Enter: " );//prompts user for platform Column A (column of starting tile)
            string response = ReadLine( );
            platformColumnA = Array.IndexOf(letters, response);
        
            if(response.Length == 0)
            {
                platformColumnA = 0;
                WriteLine("The default column is {0}", letters[platformColumnA] );//default if player A hits enter or does not enter in anything 
                check = true;
            }
        
            else if(response.Length > 1)//checks length entered
            {
                WriteLine("Error! You did not enter 1 letter.");
            }
        
            else
            {
                if (platformColumnA > columns-1)
                {
                WriteLine( "Error! Enter a valid letter for Pawn A's Platform Column. " );//ensures column is on board
                platformColumnA = Array.IndexOf(letters, response);
                }
                else check = true;
            }
        }
        
        
        check = false; 
        while(check == false)
        {
            Write( "Enter a letter for Pawn B's Platform Row or Press Enter: " );//prompts player B for platform Row B (row of starting tile)
            string response = ReadLine( );
            platformRowB = Array.IndexOf(letters, response);
       
            if(response.Length == 0)
            {
                platformRowB = positionRowB = (int) Math.Ceiling((rows-1)/2.0);//automatically sets starting position for pawn B
                WriteLine("The default row is {0}", letters[platformRowB] );
                check = true;
            }
        
            else if(response.Length > 1)//checks length entered
            {
                WriteLine("Error! You did not enter 1 letter.");
            }
        
            else
            {
                if (platformRowB > rows-1||platformRowB == platformRowA)
                {
                WriteLine( "Error!Enter a valid letter for Pawn B's Platform Row." );//ensures row is on board and not equal to platform Row A
                platformRowB = Array.IndexOf(letters, response);
                }
                else check = true;
            }
        }
       
        
        
        check = false;
        while(check == false)
        { 
    
            Write( "Enter a letter for Pawn B's Platform Column or Press Enter: " );//prompts player B for platform Column B (column of starting tile)
            string response = ReadLine( );
            platformColumnB = Array.IndexOf(letters, response);
        
            if(response.Length == 0)
            {
                platformColumnB = board.GetLength(1)-1;
                WriteLine("The default row is {0}", letters[platformColumnB] );//sets default if player B hits enter or does not enter anything
                check = true;
            }
        
            else if(response.Length > 1)//checks length entered
            {
                WriteLine("Error! You did not enter 1 letter.");
            }
        
            else
            {
                if (platformColumnB > columns-1||platformColumnB == platformColumnA)
                {
                WriteLine( "Error! Enter a valid letter for Pawn B's Platform Column." );//ensures column is on board and not equal to platform Column A
                platformColumnB = Array.IndexOf(letters, response);
                }
                else check = true;
            }
        }
 
        positionRowA = platformRowA; //sets row position of pawn A equal to the row of the starting tile
        positionColumnA=platformColumnA;//sets column position of pawn A equal to the column of the starting tile 
        positionRowB = platformRowB;//sets row position of pawn B equal to the row of the starting tile
        positionColumnB = platformColumnB;//sets column position of pawn B equal to the column of the starting tile  
    
    }
    
//Start of Gameboard

    static void DrawGameBoard ( )
    {

            const string h  = "\u2500"; // horizontal line
            const string v  = "\u2502"; // vertical line
            const string tl = "\u250c"; // top left corner
            const string tr = "\u2510"; // top right corner
            const string bl = "\u2514"; // bottom left corner
            const string br = "\u2518"; // bottom right corner
            const string vr = "\u251c"; // vertical join from right
            const string vl = "\u2524"; // vertical join from left
            const string hb = "\u252c"; // horizontal join from below
            const string ha = "\u2534"; // horizontal join from above
            const string hv = "\u253c"; // horizontal vertical cross
            //const string sp = " ";      // space
            //const string pa = "A";      // pawn A
            //const string pb = "B";      // pawn B
            const string bb = "\u25a0"; // block
            const string fb = "\u2588"; // left half block
            //const string lh = "\u258c"; // left half block
            //const string rh = "\u2590"; // right half block
            
            string[ ] letters = { "a","b","c","d","e","f","g","h","i","j","k","l",
                "m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
                
            Write( "    " ); 
            for(int l=0; l < board.GetLength(1); l++)
            {
                Write(" {0}  ", letters[l]); 
            }
            
            WriteLine();
                
            // Draw the top board boundary.
            Write( "   " );
            for( int c = 0; c < board.GetLength( 1 ); c ++ )
            {
                if( c == 0 ) Write( tl );
                Write( "{0}{0}{0}", h );
                if( c == board.GetLength( 1 ) - 1 ) Write( "{0}", tr ); 
                else                                Write( "{0}", hb );
            }
            WriteLine( );
            
            // Draw the board rows.
            for( int r = 0; r < board.GetLength( 0 ); r ++ )
            {
                Write( " {0} ", letters[ r ] );
                
                // Draw the row contents.
                for( int c = 0; c < board.GetLength( 1 ); c ++ )
                {
                    if( c == 0 ) Write( v );
                    if( board[ r, c ] ) 
                    {
                    
                        if( r == positionRowA && c == positionColumnA) Write(" A " + v); // 
                        else if( r == positionRowB && c == positionColumnB) Write(" B " + v);
                        else if( r == platformRowA && c == platformColumnA) Write(" {0} {1}", bb, v);
                        else if( r == platformRowB && c == platformColumnB) Write(" {0} {1}", bb, v );
                        else Write(" {0} {1}", fb, v);
                    
                    }
                  
                    else if (board[removableRowA,removableColumnA] ==false)//tile is set to false which means it is removed and displays as a blank space
                    {
                        Write("   "+v);
                    }
                    
                    else if (board[removableRowB, removableColumnB] ==false)//tile is set to false which means it is removed and displays as a blank space
                    {
                        Write("   "+v);
                    }
                    
                    
                }
                WriteLine( );
                
                // Draw the boundary after the row.
                if( r != board.GetLength( 0 ) - 1 )
                { 
                    Write( "   " );
                    for( int c = 0; c < board.GetLength( 1 ); c ++ )
                    {
                        if( c == 0 ) Write( vr );
                        Write( "{0}{0}{0}", h );
                        if( c == board.GetLength( 1 ) - 1 ) Write( "{0}", vl ); 
                        else                                Write( "{0}", hv );
                    }
                    WriteLine( );
                }
                else
                {
                    Write( "   " );
                    for( int c = 0; c < board.GetLength( 1 ); c ++ )
                    {
                        if( c == 0 ) Write( bl );
                        Write( "{0}{0}{0}", h );
                        if( c == board.GetLength( 1 ) - 1 ) Write( "{0}", br ); 
                        else                                Write( "{0}", ha );
                    }
                    WriteLine( );
                }
            }
            
    }//end of gameboard method
    
    //Making a new moves method 
    
    static void Moves() 
    {
    
         string[ ] letters = { "a","b","c","d","e","f","g","h","i","j","k","l",
                "m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
          string move = ""; 
          
        //player A turn
        
        bool isValid=false; 
        
        while( !isValid)
        {
            WriteLine(nameA + " it's your turn");
            Write ( "Enter four letters, two for pawn row and column and two for remove row and column: ");//prompts Player A to enter 4 letters (first two move pawn A and the other two remove a tile) 
            move = ReadLine();
            
             while (move.Length != 4)//checks to make sure the number of letters is equal to 4 
            {
                Write( "Error! You did not enter 4 letters. Please re-enter: ");//error message if player A enters more or less than 4 letters 
                move= ReadLine(); 
            }
            
            moveRowA = Array.IndexOf ( letters, move.Substring( 0, 1 ) ); //first part of move
            moveColumnA = Array.IndexOf ( letters, move.Substring( 1, 1 ) );//second part
            removableRowA= Array.IndexOf ( letters, move.Substring( 2, 1 ) );//removal of row
            removableColumnA = Array.IndexOf ( letters, move.Substring( 3, 1 ) );//removal of columns
            
            //checks if valid move
            while(moveRowA > board.GetLength(0)-1|| moveColumnA > board.GetLength(1)-1)//checks to see if move is within the board
            {
                Write("Error! Please enter a value within the board: ");
                move=ReadLine();
                
                moveRowA = Array.IndexOf ( letters, move.Substring( 0, 1 ) ); 
                moveColumnA = Array.IndexOf ( letters, move.Substring( 1, 1 ) );
                removableRowA= Array.IndexOf ( letters, move.Substring( 2, 1 ) );
                removableColumnA = Array.IndexOf ( letters, move.Substring( 3, 1 ) );
            }
            
            if ((positionRowA-1>moveRowA||moveRowA>positionRowA+1)||(positionColumnA-1>moveColumnA||moveColumnA>positionColumnA+1))//checks to see if move is to an adjacent tile
            {
                WriteLine("Error! Move is not adjacent."); 
            }
            else if(moveRowA == positionRowB&&moveColumnA == positionColumnB)//checks to see if the move is to the position of pawn B
            {
                WriteLine("Error! Pawn B is on this tile.");
            }
            else if(moveRowA==positionRowA&&moveColumnA==positionColumnA)//checks to see if the move is to position of pawn A
            {
                WriteLine("Error! You are on this tile.");
            }
            else if(board[moveRowA, moveColumnA] == false)//checks to see if tile has already been removed
            {
                WriteLine("Error! Tile has been removed.");
            }
            
            
            //valid removal
            else if((removableRowA == positionRowA && removableColumnA == positionColumnA) ||//cannot remove tile pawn A is on 
                    (removableRowA == platformRowB && removableColumnA == platformColumnB) ||//cannot remove platform B
                    (removableRowA==platformRowA&&removableColumnA==platformColumnA)||//cannot remove platform A
                    (removableRowA == moveRowA && removableColumnA == moveColumnA) ||//cannot remove tile pawn A wants to move to
                    (removableRowA == positionRowB && removableColumnA == positionColumnB) ||//cannot remove tile pawn B is on
                    (removableRowA > board.GetLength(0)-1 || removableColumnA > board.GetLength(1)-1)||//cannot remove tile that is not within the board
                    board[removableRowA, removableColumnA]==false)//cannot remove a tile that has already been removed
            {
    
                WriteLine( "Error! Tile cannot be removed."); 
                   
            }
            
            else
            {   
                positionRowA = moveRowA;//updates row position of pawn A
                positionColumnA = moveColumnA;//updates column position of pawn A
                isValid=true;
                board[removableRowA, removableColumnA] = false; 
            }

        }
        
        DrawGameBoard(); //displays gameboard
    
        //player B's turn
        
        isValid = false; 
        
        while( !isValid)
        {
            WriteLine(nameB + " it's your turn");
            Write ( "Enter four letters, two for pawn row and column and two for remove row and column: ");//prompts player B to enter 4 letters (first two are for moving pawn B and the other two is for removing a tile) 
            move = ReadLine();
            
            while (move.Length != 4)//checks to make sure the number of letters is not greater or less than 4
            {
                WriteLine( "Error! You did not enter 4 letters. Please re-enter: ");//prompts player B to re-enter if number of letters is invalid 
                move= ReadLine(); 
            }
            
            moveRowB = Array.IndexOf ( letters, move.Substring( 0, 1 ) ); //first part of move
            moveColumnB = Array.IndexOf ( letters, move.Substring( 1, 1 ) );//second part of move
            removableRowB= Array.IndexOf ( letters, move.Substring( 2, 1 ) );//removal of rows
            removableColumnB = Array.IndexOf ( letters, move.Substring( 3, 1 ) );//removal of columns
            
            //checks if it is a valid move
            while(moveRowB > board.GetLength(0)-1|| moveColumnB > board.GetLength(1)-1)//checks if move is within board
            {
                Write("Must be on board: ");
                move=ReadLine();
                
                moveRowB = Array.IndexOf ( letters, move.Substring( 0, 1 ) ); 
                moveColumnB = Array.IndexOf ( letters, move.Substring( 1, 1 ) );
                removableRowB= Array.IndexOf ( letters, move.Substring( 2, 1 ) );
                removableColumnB = Array.IndexOf ( letters, move.Substring( 3, 1 ) );
            }
            
            if ((positionRowB-1>moveRowB||moveRowB>positionRowB+1)||(positionColumnB-1>moveColumnB||moveColumnB>positionColumnB+1))//checks if move is to an adjacent tile
            {
                WriteLine("Error! Move is not adjacent."); 
            }
            
            else if(moveRowB == positionRowA&&moveColumnB == positionColumnA)//checks to see if move is to position of pawn A 
            {
                WriteLine("Error! Pawn A is on this tile.");
            }
            
            else if(moveRowB==positionRowB&&moveColumnB==positionColumnB)//checks to see if move is to position of pawn B
            {
                WriteLine("Error! You are on this tile.");
            }
            
            else if(board[moveRowB, moveColumnB] == false)//checks to see if tile has already been removed
            {
                WriteLine("Error! Tile has been removed.");
            }
            
            //checks is remove is valid
            else if((removableRowB == positionRowB && removableColumnB == positionColumnB) ||//cannot remove tile pawn B is located on 
                   (removableRowB == platformRowA && removableColumnB == platformColumnA)||//cannot remove platform of pawn A
                   (removableRowB==platformRowB&&removableColumnB==platformColumnB)||//cannot remove platform of pawn B
                   (removableRowB == moveRowB && removableColumnB == moveColumnB) ||//cannot remove tile pawn B wants to move to 
                   (removableRowB == positionRowA && removableColumnB == positionColumnA) ||//cannot remove tile pawn A is on
                   (removableRowB > board.GetLength(0)-1 || removableColumnB > board.GetLength(1)-1)||//cannot remove a tile not within board
                   board[removableRowB, removableColumnB]==false)//cannot remove a tile that has already been removed
            {
    
               WriteLine( "Error! Tile cannot be removed."); 
                   
            }
            
            else
            {   
                positionRowB = moveRowB;//updates row position of pawn B
                positionColumnB = moveColumnB;//updates column position of pawn B
                isValid=true;
                board[removableRowB, removableColumnB] = false; 
            }

        }
        
    
    }
                
                
    }   
        
}

    
    
    
    



// Graded by: Pascale Walters

/* Initialization (10)
 * + 2/2: Player's name initialization and default behaviour
 * + 2/2: Number of rows on board initialization, default behaviour, and condition check
 * + 2/2: Number of columns on board initialization, default behaviour, and condition check
 * + 2/2: Player A's starting platform initialization, default behaviour, and condition check
 * + 2/2: Player B's starting platform initialization, default behaviour, and condition check
 *
 * Board state display (10)
 * + 2/2: Each player displays correctly on the board and are distinguishable from one another
 * + 2/2: Each platform displays correctly on the board
 * + 2/2: Removed tiles display correctly on the board
 * + 2/2: Remaining (unremoved) tiles display correctly on the board
 * + 2/2: Rows and columns display nicely with no broken lines, and labels align with rows/columns
 *
 * Input Parsing (10)
 * + 2/2: Correctly parsing the input in the form of "abcd", where "ab" is the player movement tile and "cd" is the tile to remove
 * + 2/2: Detects if the player movement is valid, and updates the player position if so
 * + 2/2: Detects if the tile to remove is valid, and updates the board to reflect it if so
 * + 2/2: Passes turn to the other player only if the movement and removal are both valid and been updated to the game
 * + 2/2: Prompts the SAME user without passing the turn if invalid input is given, and no changes are updated to the game
 *
 * Style (6)
 * + 1/1: Reasonable variable names i.e. no "temp", etc.
 * + 1/1: Reasonable variable types i.e. string for name, char/int for row and column, etc.
 * + 2/2: Good commenting
 * + 2/2: Good consistent indentation and spacing
 */

// Grade: 36/36
