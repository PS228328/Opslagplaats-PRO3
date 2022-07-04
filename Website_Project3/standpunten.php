<?php 
    // De class wordt erbij gepakt.
    include_once "./classes/DeWijzerDB.php";
    $db = new DeWijzerDB;

    // Alle partijen worden opgehaald.
    if($_GET)
    {
        $rows = $db->haalAlleStandpuntenOpMetGeselecteerdThema($_GET['themas']);
    }
    else 
    {
        $rows = $db->haalAlleStandpuntenOpMetThema();
    }
    $rows2 = $db->haalAlleThemasOp();
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
    <link rel="stylesheet" href="css/DeWijzer.css">
    <link rel="stylesheet" href="css/Index.css">
    <link rel="stylesheet" href="css/HeaderFooter.css">
    <link rel="stylesheet" href="css/nav.css">
    <link rel="stylesheet" href="css/DataTable.css">
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
        <a href="standpunten.php" class="activelink">Standpunten</a>
        <a href="Verkiezingen.php">Verkiezingen</a>
        <a href="Stemwijzer.php">Stemwijzer</a>
    </nav>
    <main>
        <h1>Standpunten</h1>
        <table>
            <tr>
                <th class="hide">Id</th>
                <th class="standpunt">Standpunt</th>
                <th>Thema</th>
            </tr>
        <?php 
            foreach ($rows as $row) {
                echo "<tr>
                    <td class='hide'>$row[standpunt_id]</td>
                    <td class='standpunt'>$row[standpunt]</td>
                    <td>$row[thema]</td>
                </tr>";
            }
            ?>
        </table>
        <p>Laat alleen de standpunten zien met een bepaald thema:</p>
        <form method="GET" action="standpunten.php">
            <select name="themas">
                <option style="font-weight: bold;">Laat alles zien</option>
            <?php 
            foreach ($rows2 as $row2) {
                echo "<option>$row2[thema]</option>";
            }
            ?>
            </select>
            <button type="submit">Zoek</button>
        </form>
        <br>
        <input type="checkbox" id="ipShowId" onclick="showId(this)">
        <label for="ipShowId"><b>Laat id zien</b></label>
    </main>
    <footer>

    </footer>
</body>
</html>