/*  _____ _______         _                      _
 * |_   _|__   __|       | |                    | |
 *   | |    | |_ __   ___| |___      _____  _ __| | __  ___ ____
 *   | |    | | '_ \ / _ \ __\ \ /\ / / _ \| '__| |/ / / __|_  /
 *  _| |_   | | | | |  __/ |_ \ V  V / (_) | |  |   < | (__ / /
 * |_____|  |_|_| |_|\___|\__| \_/\_/ \___/|_|  |_|\_(_)___/___|
 *                                _
 *              ___ ___ ___ _____|_|_ _ _____
 *             | . |  _| -_|     | | | |     |  LICENCE
 *             |  _|_| |___|_|_|_|_|___|_|_|_|
 *             |_|
 *
 *   PROGRAMOVÁNÍ  <>  DESIGN  <>  PRÁCE/PODNIKÁNÍ  <>  HW A SW
 *
 * Tento zdrojový kód je součástí výukových seriálů na
 * IT sociální síti WWW.ITNETWORK.CZ
 *
 * Kód spadá pod licenci prémiového obsahu a vznikl díky podpoře
 * našich členů. Je určen pouze pro osobní užití a nesmí být šířen.
 * Více informací na http://www.itnetwork.cz/licence
 */

import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";

import InputCheck from "../components/InputCheck";
import FlashMessage from "../components/FlashMessage"
import InputSelect from "../components/InputSelect"
import InputField from "../components/InputField"

import { apiGet, apiPost, apiPut } from "../utils/api";

import Genre from "./Genre";
import {useSession} from "../contexts/session";

const MovieForm = () => {
  // parametr url adresy (id filmu), stejně jako v detailu filmu
  const { id } = useParams();
  const {session} = useSession();
  const isAdmin = session.data?.isAdmin === true;
  const isLoadingSession = session.status === "loading";
  useEffect(() => {
    if (!isAdmin && !isLoadingSession) {
      if (id) {
        navigate("/movies/show/" + id);
      } else {
        navigate("/movies");
      }
    }
  }, [isAdmin, isLoadingSession, id]);
  const navigate = useNavigate();
  
  const [directorListState, setDirectorList] = useState([]);
  const [actorListState, setActorList] = useState([]);
  const [genreListState, setGenreList] = useState([]);
  const [movieNameState, setMovieName] = useState("");
  const [yearState, setYear] = useState(0);
  const [directorState, setDirector] = useState("");
  const [actorsState, setActors] = useState([]);
  const [genresState, setGenres] = useState([]);
  const [availableState, setAvailable] = useState(false);
  const [sentState, setSent] = useState(false);
  const [successState, setSuccess] = useState(false);
  const [errorState, setError] = useState();


  const handleChange = (e) => {
    const target = e.target;

    let temp;
    if (["actors", "genres"].includes(target.name)) {
      temp = Array.from(target.selectedOptions, (item) => item.value);
    } else if (target.name === "available") {
      temp = target.checked;
    } else {
      temp = target.value;
    }

    const name = target.name;
    const value = temp;

    if (name === "movieName") {
      console.log(value);
      setMovieName(value);
    } else if (name === "year") {
      console.log(value);
      setYear(value);
    } else if (name === "director") {
      console.log(value);
      setDirector(value);
    } else if (name === "actors") {
      console.log(value);
      setActors(value);
    } else if (name === "genres") {
      console.log(value);
      setGenres(value);
    } else if (name === "available") {
      console.log(value);
      setAvailable(value);
    }
  };

  const handleSubmit = (e) => {
    e.preventDefault();

    const body = {
      name: movieNameState,
      year: yearState,
      directorID: directorState,
      actorIDs: actorsState,
      genres: genresState,
      isAvailable: availableState,
    };

    (id ? apiPut("/api/movies/" + id, body) : apiPost("/api/movies/", body))
      .then((data) => {
        console.log("succcess", data);
        setSent(true);
        setSuccess(true);
        navigate("/movies");
      })
      .catch((error) => {
        console.log(error.message);
        setError(error.message);
        setSent(true);
        setSuccess(false);
      });
  };

  useEffect(() => {
    if (id) {
      apiGet("/api/movies/" + id).then((data) => {
        setMovieName(data.name);
        setYear(data.year);
        setDirector(data.directorID);
        setActors(data.actorIDs);
        setGenres(data.genres);
        setAvailable(data.isAvailable);
      });
    }

    apiGet("/api/directors").then((data) => setDirectorList(data));
    apiGet("/api/actors").then((data) => setActorList(data));
    apiGet("/api/genres").then((data) => setGenreList(data));
  }, [id]);

  if (!isAdmin) {
    console.log(session, session.data?.isAdmin, isAdmin)
    return (
        <div className="d-flex justify-content-center mt-2">
          <div className="spinner-border spinner-border-sm" role="status">
            <span className="visually-hidden">Loading...</span>
          </div>
        </div>
    );
  }

  const sent = sentState;
  const success = successState;

  return (
    <div>
      <h1>{id ? "Upravit" : "Vytvořit"} film</h1>
      <hr />
      {errorState ? (
        <div className="alert alert-danger">{errorState}</div>
      ) : null}

      {sent && success ? (
        <FlashMessage
          theme={"success"}
          text={"Uložení filmu proběhlo úspěšně."}
        />
      ) : null}

      <form onSubmit={handleSubmit}>
        <InputField
          required={true}
          type="text"
          name="movieName"
          min="3"
          label="Název"
          prompt="Zadejte název díla"
          value={movieNameState}
          handleChange={handleChange}
        />

        <InputField
          required={true}
          type="number"
          name="year"
          label="Rok vydání"
          prompt="Zadejte rok vydání"
          min="0"
          value={yearState}
          handleChange={handleChange}
        />

        <InputSelect
          name="director"
          items={directorListState}
          label="Režie"
          prompt="Vyberte režiséra"
          value={directorState}
          handleChange={handleChange}
        />

        <InputSelect
          required={true}
          name="actors"
          items={actorListState}
          multiple={true}
          label="Hrají"
          prompt="Označte herce"
          value={actorsState}
          handleChange={handleChange}
        />

        <InputSelect
          required={true}
          name="genres"
          items={genreListState}
          multiple={true}
          enum={Genre}
          label="Žánr"
          prompt="Označte žánry"
          value={genresState}
          handleChange={handleChange}
        />

        <InputCheck
          type="checkbox"
          name="available"
          label="Dostupný"
          value={availableState}
          checked={availableState}
          handleChange={handleChange}
        />

        <input type="submit" className="btn btn-primary" value="Uložit" />
      </form>
    </div>
  );
};

export default MovieForm;
