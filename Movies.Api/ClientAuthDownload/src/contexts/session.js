import {createContext, useContext, useEffect, useState} from "react";
import {apiGet, HttpRequestError} from "../utils/api";

const SessionContext = createContext({
    session: {data: null, status: "loading"}, setSession: (data) => {
    }
});

export function useSession() {
    return useContext(SessionContext);
}

export const SessionProvider = ({children}) => {
    const [sessionState, setSessionState] = useState({data: null, status: "loading"})
    useEffect(() => {
        apiGet("/api/auth")
            .then(data => setSessionState({data, status: "authenticated"}))
            .catch(e => {
                if (e instanceof HttpRequestError && e.response.status === 401) {
                    setSessionState({data: null, status: "unauthenticated"});
                } else {
                    throw e;
                }
            });
    }, []);

    return (
        <SessionContext.Provider value={{session: sessionState, setSession: setSessionState}}>
            {children}
        </SessionContext.Provider>
    )
};