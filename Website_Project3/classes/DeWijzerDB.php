<?php
    class DeWijzerDB {
        const DSN = "mysql:host=localhost; dbname=verkiezingenprj3;";
        const USER = "root";
        const PASSWD = "";

        function partijenOphalen($filter) {
            $pdo = new PDO(self::DSN, self::USER, self::PASSWD);

            if ($filter != "") {
                $filter = "INNER JOIN partij_verkiezing pv ON partij.partij_id = pv.partij_id
                WHERE verkiezingID = $filter";
            }
            $statement = $pdo->prepare("SELECT partij.partij_id AS id, naam, adres, postcode, gemeente, emailadres AS email, telefoonnummer AS nummer
                FROM partij
                $filter");
            $statement->execute();

            return $statement;
        }

        function verkiezingenOphalen() {
            $pdo = new PDO(self::DSN, self::USER, self::PASSWD);

            $statement = $pdo->prepare("SELECT v.verkiezing_id AS id, datum AS verkiezingdatum, verkiezingsoort, COUNT(pv.partij_id) AS AantalPartijen
                FROM verkiezing v
                INNER JOIN verkiezingsoort vs ON v.verkiezingsoortID = vs.verkiezingsoort_id
                INNER JOIN partij_verkiezing pv ON v.verkiezing_id = pv.verkiezingID
                GROUP BY v.verkiezing_id");
            $statement->execute();

            return $statement;
        }

        function standpuntenOphalen() {
            $pdo = new PDO(self::DSN, self::USER, self::PASSWD);

            $statement = $pdo->prepare("SELECT * FROM standpunt");
            $statement->execute();

            return $statement;
        }

        function partijstandpuntOphalen()
        {
            $pdo = new PDO(self::DSN, self::USER, self::PASSWD);

            $statement = $pdo->prepare("SELECT * FROM partij_standpunt");
            $statement->execute();

            return $statement;
        }

        function haalAllePartijenOp()
        {
            $pdo = new PDO(self::DSN, self::USER, self::PASSWD);

            $statement = $pdo->prepare("SELECT * FROM partij");
            $statement->execute();

            return $statement;
        }

        function haalEenPartijOp($id)
        {
            $pdo = new PDO(self::DSN, self::USER, self::PASSWD);

            $statement = $pdo->prepare("SELECT naam FROM partij WHERE partij_id = :id");
            $statement->bindValue(":id",$id,PDO::PARAM_INT);
            $statement->execute();

            return $statement;
        }

        function haalAlleThemasOp()
        {
            $pdo = new PDO(self::DSN, self::USER, self::PASSWD);

            $statement = $pdo->prepare("SELECT * FROM thema");
            $statement->execute();

            return $statement;
        }

        function haalAlleStandpuntenOpMetThema()
        {
            $pdo = new PDO(self::DSN, self::USER, self::PASSWD);

            $statement = $pdo->prepare("SELECT s.standpunt_id, t.thema, s.standpunt FROM standpunt s INNER JOIN thema t ON t.thema_id = s.thema_id");
            $statement->execute();

            return $statement;
        }

        function haalAlleStandpuntenOpMetGeselecteerdThema($thema)
        {
            $pdo = new PDO(self::DSN, self::USER, self::PASSWD);

            if($thema == "Laat alles zien")
            {
                $statement = $pdo->prepare("SELECT s.standpunt_id, t.thema, s.standpunt FROM standpunt s INNER JOIN thema t ON t.thema_id = s.thema_id");
            }
            else
            {
                $statement = $pdo->prepare("SELECT s.standpunt_id, t.thema, s.standpunt FROM standpunt s INNER JOIN thema t ON t.thema_id = s.thema_id WHERE t.thema = :thema");
                $statement->bindValue(":thema",$thema,PDO::PARAM_STR);
            }
            $statement->execute();
            return $statement;
        }
    }
?>