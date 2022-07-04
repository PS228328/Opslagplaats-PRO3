<?php 
    // De class wordt erbij gepakt.
    include_once "./classes/DeWijzerDB.php";
    $db = new DeWijzerDB;

    // Als de partijen van een bapaalde verkiezing moeten zijn, dan wordt die verkiezing en de soort ervan in een variabele gezet.
    if (isSet($_GET["verkiezing"])) {
        $filter = $_GET['verkiezing'];
        $vSoort = "voor de $_GET[vSoort]";
    }
    else {
        $filter = "";
        $vSoort = "";
    }

    // Alle partijen worden opgehaald.
    $rows = $db->partijenOphalen($filter);
?>

<!DOCTYPE html>
<html lang="nl">

<head>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>De Wijzer - Partijen</title>
    <link rel="stylesheet" href="css/DeWijzer">
    <link rel="stylesheet" href="css/DataTable">
    <link rel="stylesheet" href="css/HeaderFooter">
    <link rel="stylesheet" href="css/nav">
    <script src="js/DeWijzer.js"></script>
</head>
<body>
    <header>
        De Wijzer &nbsp;
        <img src="img/Logo.png" alt="Hier hoort het logo te staan">
    </header>
    <nav>
        <a href="Index.html">Home</a>
        <a href="Partijen.php" class="activelink">Partijen</a>
        <a href="themas.php">Thema's</a>
        <a href="standpunten.php">Standpunten</a>
        <a href="Verkiezingen.php">Verkiezingen</a>
        <a href="Stemwijzer.php">Stemwijzer</a>
    </nav>
    <main>
        <h1>Partijen <?= $vSoort ?></h1>

        <?php if ($filter != "") {
            // Stuurd je naar deze pagina maar zonder get.
            echo "<a href='Partijen.php'>Alle partijen weergeven</a>
            <br> <br>";
        }?>

        <table>
            <tr>
                <th class="hide">Id</th>
                <th>Naam</th>
                <th>Adres</th>
                <th>Postcode</th>
                <th>Gemeente</th>
                <th>Email</th>
                <th>Telefoon Nummer</th>
            </tr>
            <?php 
                // Er word voor elke partij een rij gemaakt in de tabel.
                foreach ($rows as $row) {
                    echo "<tr>
                        <td class='hide'>$row[id]</td>
                        <td>$row[naam]</td>
                        <td>$row[adres]</td>
                        <td>$row[postcode]</td>
                        <td>$row[gemeente]</td>
                        <td>$row[email]</td>
                        <td>$row[nummer]</td>
                    </tr>";
                }
            ?>
        </table>

        <br><br>

        <input type="checkbox" id="ipShowId" onclick="showId(this)">
        <label for="ipShowId"><b>Laat id zien</b></label>
    </main>
    <footer>

    </footer>
</body>

</html>