INSERT INTO public.cities (id, "name") VALUES
	 (1,'Atlanta'),
	 (2,'Boston'),
	 (3,'Charlotte'),
	 (4,'Chicago'),
	 (5,'Cleveland'),
	 (6,'Detroit'),
	 (7,'Los Angeles');

INSERT INTO public.nbateams (id, "name",cityid) VALUES
	 (1,'Hawks',1),
	 (2,'Celtics',2),
	 (3,'Hornets',3),
	 (4,'Bulls',4),
	 (5,'Cavaliers',5),
	 (6,'Pistons',6),
	 (7,'Clippers',7),
	 (8,'Lakers',7);

INSERT INTO public.players (birthday,firstname,lastname,nbateamid) VALUES
	 ('1963-02-17','Michael','Jordan',4),
	 ('1965-09-25','Scottie','Pippen',4),
	 ('1961-05-13','Dennis','Rodman',4),
     ('1964-01-20','Ron','Harper',4),
	 ('1956-12-07','Larry','Bird',2),
	 ('1957-12-19','Kevin','McHale',2),
	 ('1953-08-30','Robert','Parish',2),
	 ('1960-01-12','Dominique','Wilkins',1),
	 ('1963-07-13','Spud','Webb',1);


