<?php 
    // De class wordt erbij gepakt.
    include_once "./classes/DeWijzerDB.php";
    $db = new DeWijzerDB;

    // Alle partijen worden opgehaald.
    $rows = $db->standpuntenOphalen();
?>

<!DOCTYPE html>
<html lang="nl">
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>De Wijzer - Stemwijzer</title>
    <link rel="stylesheet" href="css/DeWijzer.css">
    <link rel="stylesheet" href="css/Stemwijzer">
    <link rel="stylesheet" href="css/HeaderFooter.css">
    <link rel="stylesheet" href="css/nav.css">
</head>
<body>
    <header>
        De Wijzer &nbsp;
        <img src="img/Logo.png" alt="Hier hoort het logo te staan">
    </header>
    <nav>
        <a href="Index.html">Home</a>
        <a href="Partijen.php">Partijen</a>
        <a href="themas.php">Thema's</a>
        <a href="standpunten.php">Standpunten</a>
        <a href="Verkiezingen.php">Verkiezingen</a>
        <a href="Stemwijzer.php" class="activelink">Stemwijzer</a>
    </nav>
    <main>
        <h1>De stemwijzer</h1>

        <?php
            if (!$_POST) {
                echo "<p> 
                    Welkom bij de stemwijzer, hier kunt u uw mening geven over standpunten, en daardoor wordt bepaald welke partij het meeste bij u past. Klik op 
                    de knop om te beginnen
                </p>";
            }
        ?>

        <form action="" method="POST">
            <?php
                if (!$_POST) {
                    echo "<input type='submit' name='begin' value='Beginnen'>";
                    //Op het begin is er nog niks gepost, dus dan krijgen we alleen een button te zien waarop geklikt kan worden
                }
                else 
                {
                    //Na de click komen we natuurlijk in dit blok, hierbinnen komen we eerst in de else
                    if($_POST['begin'] == "Verstuur")
                    {
                        $db = new DeWijzerDB;
                        $rows = $db->partijstandpuntOphalen();

                        $db2 = new DeWijzerDB;
                        $rows2 = $db2->haalAllePartijenOp();

                        $db3 = new DeWijzerDB;
                        $rows3 = $db3->standpuntenOphalen();

                        $counter = 0;

                        foreach($rows3 as $row3)
                        {
                            $counter++;
                            //Deze counter kijkt hoeveel standpunten er in totaal zijn
                        }

                        $partijen = array();

                        foreach($rows2 as $row2)
                        {
                            $partijen[$row2['partij_id']] = 0;
                        }

                        foreach($rows as $row)
                        {
                            if($row['mening'] == $_POST[$row['standpunt_id']])
                            {
                                $partijen[$row['partij_id']] += 1;
                                //Wanneer de mening van een partij hetzelfde is als de mening die de gebruiker heeft ingegeven
                                //Dan word er 1 bij opgeteld in de asso-array
                            }
                        }

                        echo "<h2 class=gridspan style=border: none;>Uw eindscore (Op volgorde van meeste naar minste overeenkomsten)</h2><br>";
                        
                        arsort($partijen);
                        //We sorteren de array van hoog naar laag

                        foreach($partijen as $x=>$x_value)
                        {
                            $db3 = new DeWijzerDB;
                            $rows3 = $db3->haalEenPartijOp($x);

                            //hier lopen we door de array heen, en telkens halen we de partij op met de key (het id van de partij)
                            $procentscore = $x_value / $counter * 100;
                            //Elke partij heeft natuurlijk een aantal punten gekregen om te vergelijken, dat is de x_value

                            foreach($rows3 as $row3)
                            {
                                echo "<p class=gridspan>Met partij <b style='color: green; font-size: 18px;'>" . $row3['naam'] . "</b> heeft u over <b style='color: brown; font-size: 18px;'>" . $x_value . " van de " . $counter ." 
                                stellingen</b> dezelfde mening - <b style='font-size: 22px;'>" . round($procentscore) . "% overeenkomst</b></p>";
                            }
                        }
                        
                    }
                    else
                    {
                        //Alle standpunten worden ingeladen, inclusief een select waarbij de gebruiker een mening kan geven
                        $border1 = 0;
                        $border2 = 0;
                        foreach ($rows as $row) {
                            $border1++;
                            $border2++;
                            $corner = "";
                            
                            if ($border1 == 1 && $border2 == 1) {
                                $corner = "class= 'corner1 corner2'";
                            }
                            else if ($border1 == 1) {
                                $corner = "class= 'corner1'";
                            }
                            else if ($border2 == 1) {
                                $corner = "class= 'corner2'";
                            }
    
                            if ($border1 == 2) {
                                $border1 = 0;
                            }
                            if ($border2 == 3) {
                                $border2 = 0;
                            }
                            
                            echo "<div $corner>
                                <label for='sl$row[standpunt_id]'>$row[standpunt]</label> <br>
                                <select name='$row[standpunt_id]' id='sl$row[standpunt_id]'>
                                    <option value='1'>Mee eens</option>
                                    <option value='0'>Mee oneens</option>
                                </select>
                            </div>";
                        }
                        echo "<input type='submit' name='begin' value='Verstuur'>";
                    }              
                }
            ?>
        </form>
    </main>
    <footer>

    </footer>
</body>
</html>