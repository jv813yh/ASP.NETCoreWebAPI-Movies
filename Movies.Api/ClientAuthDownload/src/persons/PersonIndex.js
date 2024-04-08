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

import { apiGet, apiDelete } from "../utils/api";

import PersonTable from "./PersonTable";
import PersonFilter from "./PersonFilter";

const PersonIndex = () => {
    const [directorsState, setDirectors] = useState([]);
    const [actorsState, setActors] = useState([]);

    const [directorLimitState, setDirectorLimit] = useState("");
    const [actorLimitState, setActorLimit] = useState("");

    const moreActors = Boolean(actorsState.length > directorsState.length);

    const deleteDirector = async (id) => {
        try {
            await apiDelete("/api/people/" + id);
            setDirectors(directorsState.filter((item) => item._id !== id));
        } catch (error) {
            console.log(error.message);
            alert(error.message)
        }
    };

    const deleteActor = async (id) => {
        try {
            await apiDelete("/api/people/" + id);
        } catch (error) {
            console.log(error.message);
            alert(error.message)
        }
        setActors(actorsState.filter((item) => item._id !== id));
    };

    useEffect(() => {
        apiGet("/api/directors").then((data) => setDirectors(data));
        apiGet("/api/actors").then((data) => setActors(data));
    }, []);

    const handleChange = (e) => {
        let name = e.target.name;
        let value = e.target.value;
        if (name === "actorLimit") setActorLimit(value);
        else if (name === "directorLimit") setDirectorLimit(value);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const directorLimit = parseInt(directorLimitState);
        const actorLimit = parseInt(actorLimitState);

        if (directorLimit) {
            const params = {
                limit: directorLimitState,
            };

            await apiGet("/api/directors", params).then((data) =>
                setDirectors(data)
            );
        }

        if (actorLimit) {
            const params = {
                limit: actorLimitState,
            };
            await apiGet("/api/actors", params).then((data) => setActors(data));
        }
    };

    return (
        <div>
            <h1>Seznam osobností</h1>
            <div className="row">
                <div className="col">
                    <PersonFilter
                        name={"actorLimit"}
                        handleChange={handleChange}
                        handleSubmit={handleSubmit}
                        value={actorLimitState}
                        label="Limit počtu herců"
                        confirm="Filtrovat herce"
                    />
                </div>
                <div className="col">
                    <PersonFilter
                        name={"directorLimit"}
                        handleChange={handleChange}
                        handleSubmit={handleSubmit}
                        value={directorLimitState}
                        label="Limit počtu režisérů"
                        confirm="Filtrovat režiséry"
                    />
                </div>
            </div>
            <hr />
            <div className="row">
                <div className="col">
                    <PersonTable
                        deletePerson={deleteActor}
                        link={!moreActors}
                        items={actorsState}
                        label="Počet herců:"
                    />
                </div>
                <div className="col">
                    <PersonTable
                        deletePerson={deleteDirector}
                        link={moreActors}
                        items={directorsState}
                        label="Počet režisérů:"
                    />
                </div>
            </div>
        </div>
    );
};
export default PersonIndex;
