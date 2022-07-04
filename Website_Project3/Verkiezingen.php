<?php 
    // De class wordt erbij gepakt.
    include_once "./classes/DeWijzerDB.php";
    $db = new DeWijzerDB;

    // Alle partijen worden opgehaald.
    $rows = $db->verkiezingenOphalen();
?>

<!DOCTYPE html>
<html lang="nl">

<head>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>De Wijzer - Verkiezingen</title>
    <link rel="stylesheet" href="css/DeWijzer.css">
    <link rel="stylesheet" href="css/DataTable.css">
    <link rel="stylesheet" href="css/HeaderFooter.css">
    <link rel="stylesheet" href="css/nav.css">
    <script src="js/DeWijzer.js"></script>
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
        <a href="Verkiezingen.php" class="activelink">Verkiezingen</a>
        <a href="Stemwijzer.php">Stemwijzer</a>
    </nav>
    <main>
        <h1>Partijen</h1>

        <table>
            <tr>
                <th class="hide">Id</th>
                <th>Verkiezingsoort</th>
                <th>Verkiezingdatum</th>
                <th>Aantal Partijen</th>
            </tr>
            <?php 
                // Er word voor elke partij een rij gemaakt in de tabel.
                foreach ($rows as $row) {
                    echo "<tr>
                        <td class='hide'>$row[id]</td>
                        <td>$row[verkiezingsoort]</td>
                        <td>$row[verkiezingdatum]</td>
                        <td>$row[AantalPartijen] &nbsp; <a href='Partijen.php?verkiezing=$row[id]&vSoort=$row[verkiezingsoort]'>Weergeven</a></td>
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