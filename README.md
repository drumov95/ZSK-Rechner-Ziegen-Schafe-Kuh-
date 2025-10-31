# ZSK-Rechner (Ziegen-Schafe-Kuh GmbH & Co. KG)

Ein ASP.NET Core MVC-Projekt zur Berechnung des „Werts“ eines Social-Media-Accounts anhand tierischer Umrechnungseinheiten 
Das Projekt wurde im Rahmen eines Softwareentwicklungsprojekts entwickelt und kombiniert **moderne Webtechnologien**, **Datenbankanbindung** und **ORM (Entity Framework Core)**.

---

##  Features

-  **Euro → Tiere** – Berechnet, wie viele Tiere man für einen bestimmten Eurobetrag erhält  
-  **Tiere → Euro (Mehrfach)** – Mehrere Tierarten gleichzeitig eingeben und Gesamtwert berechnen  
-  **Wechselgeld-Logik** – Automatische Aufteilung eines Eurobetrags in Tierwerte (ähnlich Münzwechsel-Algorithmus)  
-  **Tierverwaltung (CRUD)** – Tiere hinzufügen, bearbeiten oder löschen  
-  **Archivierung aller Umrechnungen** – Jede Umrechnung wird in der Datenbank gespeichert  

---

##  Architektur & Technologien

| Bereich | Technologie |
|----------|--------------|
| Framework | ASP.NET Core MVC |
| ORM | Entity Framework Core (Code-First Ansatz) |
| Frontend | Razor Views, HTML, CSS, Bootstrap |
| Datenbank | Microsoft SQL Server LocalDB |
| Architekturmuster | Model-View-Controller (MVC) |

---

## 🗄️ Datenmodell

**Entitys:**

- **AnimalRate**   
  Enthält Name und Euro-Wert eines Tiers (z. B. Kuh = 2800 €, Schaf = 650 €).  

- **Conversion**   
  Speichert alle Umrechnungen mit Richtung, Datum und Kurswert.

**Beziehung:**  
Eine *AnimalRate* kann mehrere *Conversions* haben → **1:n-Beziehung**  
(Beim Löschen einer Tierart bleiben alte Umrechnungen erhalten dank `DeleteBehavior.Restrict`)
