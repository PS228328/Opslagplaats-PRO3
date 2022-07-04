<?php 
    // De class wordt erbij gepakt.
    include_once "./classes/DeWijzerDB.php";
    $db = new DeWijzerDB;

    // Alle partijen worden opgehaald.
    $rows = $db->haalAlleThemasOp();
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
        <a href="themas.php" class="activelink">Thema's</a>
        <a href="standpunten.php">Standpunten</a>
        <a href="Verkiezingen.php">Verkiezingen</a>
        <a href="Stemwijzer.php">Stemwijzer</a>
    </nav>
    <main>
        <h1>Thema's</h1>
        <table>
            <tr>
                <th class="hide">Id</th>
                <th>Overzicht</th>
            </tr>
        <?php 
            foreach ($rows as $row) {
                echo "<tr>
                    <td class='hide'>$row[thema_id]</td>
                    <td>$row[thema]</td>
                </tr>";
            }
            ?>
        </table>
        <input type="checkbox" id="ipShowId" onclick="showId(this)">
        <label for="ipShowId"><b>Laat id zien</b></label>
    </main>
    <footer>

    </footer>
</body>
</html>