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

import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";
import {
  BrowserRouter as Router,
  Link,
  Route,
  Routes,
  Navigate,
} from "react-router-dom";

import MovieIndex from "./movies/MovieIndex";
import PersonIndex from "./persons/PersonIndex";
import MovieDetail from "./movies/MovieDetail";
import PersonDetail from "./persons/PersonDetail";
import MovieForm from "./movies/MovieForm";
import PersonForm from "./persons/PersonForm";
import {RegistrationPage} from "./registration/RegistrationPage";
import {useSession} from "./contexts/session";
import {apiDelete} from "./utils/api";
import {LoginPage} from "./login/LoginPage";

export function App() {
  const {session, setSession} = useSession();
  const handleLogoutClick = () => {
    apiDelete("/api/auth")
        .finally(() => setSession({data: null, status: "unauthorized"}));
  }
  return (
    <Router>
      <div className="container">
        <nav className="navbar navbar-expand-lg navbar-light bg-light justify-content-between">
          <ul className="navbar-nav mr-auto">
            <li className="nav-item">
              <Link to={"/movies"} className="nav-link">
                Filmy
              </Link>
            </li>
            <li className="nav-item">
              <Link to={"/people"} className="nav-link">
                Osobnosti
              </Link>
            </li>
          </ul>
          <ul className="navbar-nav align-items-center gap-2">
            {session.data ? <>
              <li className="nav-item">{session.data.email}</li>
              <li className="nav-item">
                <button className="btn btn-sm btn-secondary" onClick={handleLogoutClick}>Odhlásit se</button>
              </li>
            </> : session.status === "loading" ?
                <>
                  <div className="spinner-border spinner-border-sm" role="status">
                    <span className="visually-hidden">Loading...</span>
                  </div>
                </>
                :<>
                  <li className="nav-item">
                    <Link to={"/register"}>Registrace</Link>
                  </li>
                  <li className="nav-item">
                    <Link to={"/login"}>Přihlásit se</Link>
                  </li>
                </>
            }
          </ul>
        </nav>

        <Routes>
          <Route index element={<Navigate to={"/movies"} />} />
          <Route path="/movies">
            <Route index element={<MovieIndex />} />
            <Route path="show/:id" element={<MovieDetail />} />
            <Route path="create" element={<MovieForm />} />
            <Route path="edit/:id" element={<MovieForm />} />
          </Route>
          <Route path="/people">
            <Route index element={<PersonIndex />} />
            <Route path="show/:id" element={<PersonDetail />} />
            <Route path="create" element={<PersonForm />} />
            <Route path="edit/:id" element={<PersonForm />} />
          </Route>
          <Route path="/register" element={<RegistrationPage/>}/>
          <Route path="/login" element={<LoginPage/>}/>
        </Routes>
      </div>
    </Router>
  );
}

export default App;
