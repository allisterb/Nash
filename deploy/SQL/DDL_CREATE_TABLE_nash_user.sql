-- SEQUENCE: public.nash_user__id_seq

-- DROP SEQUENCE public.nash_user__id_seq;

CREATE SEQUENCE public.nash_user__id_seq
    INCREMENT 1
    START 1
    MINVALUE 1
    MAXVALUE 2147483647
    CACHE 1;

ALTER SEQUENCE public.nash_user__id_seq
    OWNER TO nash;

-- Table: public.nash_user

-- DROP TABLE public.nash_user;

CREATE TABLE public.nash_user
(
    _id integer NOT NULL DEFAULT nextval('nash_user__id_seq'::regclass),
    user_name character varying(100) COLLATE pg_catalog."default" NOT NULL,
    last_logged_in timestamp without time zone,
    CONSTRAINT nash_user_pkey PRIMARY KEY (_id)
)

TABLESPACE pg_default;

ALTER TABLE public.nash_user
    OWNER to nash;
