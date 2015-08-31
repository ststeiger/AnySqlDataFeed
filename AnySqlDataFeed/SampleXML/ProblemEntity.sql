
CREATE TABLE dbo.T_AP_Kunst3
(
	 KU_UID uniqueidentifier NOT NULL
	,KU_Nr character varying(25) NULL
	,KU_Abmessung character varying(50) NULL
	,KU_Bemerkung text NULL
	,KU_AusgeliehenAm datetime NULL
	,KU_AusgeliehenAn national character varying(100) NULL
	,KU_AusgeliehenBemerkung text NULL
	,KU_IsMobil bit NOT NULL
	,KU_Versicherungswert float NOT NULL
	,KU_Status integer NOT NULL
	,CONSTRAINT PK_T_AP_Kunst3 PRIMARY KEY ( KU_UID )
);

GO 


INSERT INTO dbo.T_AP_Kunst3 (KU_UID, KU_Nr, KU_Abmessung, KU_Bemerkung, KU_AusgeliehenAm, KU_AusgeliehenAn, KU_AusgeliehenBemerkung, KU_IsMobil, KU_Versicherungswert, KU_Status) VALUES (N'fe01f80f-8c2f-42f3-a046-c3f183591c9c', N'1001', N'79,0 x 38,0 auf 89,0 x 61,0', N'Geneviève Claisse 

Die französische Künstlerin Geneviève Claisse (* 1935) ist eine Grande 
Dame der Geometrischen Abstraktion. In den vergangenen 50 Jahren hat 
sie ein ebenso umfangreiches wie beeindruckendes Werk geschaffen, das 
vor allem durch eine feinsinnige Balance der Farben und Formen besticht. 
Ausgehend von anfangs dichten, farbsatten Kompositionen fand sie zu einer 
immer puristischeren Bildästhetik. Geneviève Claisse, die außer Gemälden 
auch Plastiken und Reliefs schuf, verfolgte von Anbeginn ihrer Künstlerlauf-
bahn eine autonome, ungegenständliche Malerei. Die geistigen Wurzeln ihrer 
Kunst liegen in den frühen konstruktiv-geometrischen Bewegungen, vornehmlich
 in Kasimir Malewitschs Ideologie der Gegenstandslosigkeit, dem Suprematismus
.Wichtige Anregungen erhielt sie als junge Künstlerin zudem von dem Maler
 Auguste Herbin, 

dessen Assistentin sie in den späten 1950er Jahren war
Auf dieser Grundlage entwickelte sie einen ganz eigenen, unverwechselbaren Stil. Schon die frühesten erhaltenen Skizzenbücher und Gouachen der Künstlerin aus den 1950er Jahren lassen erkennen, dass sie ganz eigenständige Wege beschreiten würde. Bereits in diesen Blättern ist das bis heute für Geneviève Claisse so charakteristische sichere Gespür für Farb- und Formakkorde vollständig ausgeprägt. In ihrer künstlerischer Entwicklung folgen Werkphasen mit spitzen, schwarzen Dreiecksformen, die zu fragil und leicht wirkenden Kompositionen angeordnet sind, später dann mit großen, farbigen Kreisen und kinetischen Linienstrukturen in Schwarzweiß. Daneben taucht im Œuvre der Künstlerin bis heute immer wieder das Quadrat als zentrales Bildelement auf, wobei es mal als statisch gefestigte und mal als scheinbar sanft pulsierende, im Bildraum schwebende Form erscheint. ', CAST(0x0000A4AB00000000 AS DateTime), N'COR2', N'Test Ausleihe bndsgg', 0, 100.23, 1)
;
