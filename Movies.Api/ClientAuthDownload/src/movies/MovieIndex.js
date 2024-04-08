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
import { apiDelete, apiGet } from "../utils/api";

import MovieTable from "./MovieTable";
import MovieFilter from "./MovieFilter";

const MovieIndex = () => {
  const [directorListState, setDirectorList] = useState([]);
  const [actorListState, setActorList] = useState([]);
  const [genreListState, setGenreList] = useState([]);
  const [moviesState, setMovies] = useState([]);
  const [filterState, setFilter] = useState({
    directorID: undefined,
    actorID: undefined,
    genre: undefined,
    fromYear: undefined,
    toYear: undefined,
    limit: undefined,
  });

  useEffect(() => {
    apiGet("/api/directors").then((data) => setDirectorList(data));
    apiGet("/api/actors").then((data) => setActorList(data));
    apiGet("/api/genres").then((data) => setGenreList(data));
    apiGet("/api/movies").then((data) => setMovies(data));
  }, []);

  const deleteMovie = async (id) => {
    await apiDelete("/api/movies/" + id);
    setMovies(moviesState.filter((movie) => movie._id !== id));
  };

  const handleChange = (e) => {
    console.log(e.target.value);
    // pokud vybereme prázdnou hodnotu (máme definováno jako true/false/'' v komponentách), nastavíme na undefined
    if (
      e.target.value === "false" ||
      e.target.value === "true" ||
      e.target.value === ""
    ) {
      setFilter((prevState) => {
        return { ...prevState, [e.target.name]: undefined };
      });
    } else {
      setFilter((prevState) => {
        return { ...prevState, [e.target.name]: e.target.value };
      });
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const params = filterState;

    const data = await apiGet("/api/movies", params);
    setMovies(data);
  };

  return (
    <div>
      <h1>Seznam filmů</h1>
      <hr />
      <MovieFilter
        handleChange={handleChange}
        handleSubmit={handleSubmit}
        directorList={directorListState}
        actorList={actorListState}
        genreList={genreListState}
        filter={filterState}
        confirm="Filtrovat filmy"
      />
      <hr />
      <MovieTable
        deleteMovie={deleteMovie}
        items={moviesState}
        label="Počet filmů:"
      />
    </div>
  );
};

export default MovieIndex;
