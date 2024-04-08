import {apiPost, HttpRequestError} from "../utils/api";
import {useNavigate} from "react-router-dom";
import {useState} from "react";
import InputField from "../components/InputField";
import FlashMessage from "../components/FlashMessage";


export const RegistrationPage = () => {
    const nav = useNavigate();
    const [errorMessageState, setErrorMessageState] = useState(null);
    const [valuesState, setValuesState] = useState({password: "", confirmPassword: "", email: ""});
    const handleSubmit = (e) => {
        e.preventDefault();
        if (valuesState.password !== valuesState.confirmPassword) {
            setErrorMessageState("Hesla se nerovnají");
            return;
        }
        const {confirmPassword, ...registrationData} = valuesState;
        apiPost("/api/user", registrationData)
            .then(() => {
                nav("/login");
            }).catch(e => {
            if(e instanceof HttpRequestError && e.response.status === 400) {
                e.response.text().then(message => setErrorMessageState(message));
                return;
            }
            setErrorMessageState("Při komunikaci se serverem nastala chyba.");
        });
    };
    const handleChange = (e) => {
        const fieldName = e.target.name;
        setValuesState({...valuesState, [fieldName]: e.target.value});
    };
    return (
        <div className="offset-4 col-sm-6 mt-5">
            <h1>Registrace</h1>
            <form onSubmit={handleSubmit}>
                {errorMessageState ? <FlashMessage theme={"danger"} text={errorMessageState}></FlashMessage> : null}
                <InputField type="email" name="email" label="E-mail" prompt="Zadejte Váš E-mail" value={valuesState.email}
                            handleChange={handleChange}/>
                <InputField type="password" name="password" label="Heslo" prompt="Zadejte Vaše heslo" min={6} value={valuesState.password}
                            handleChange={handleChange}/>
                <InputField type="password" name="confirmPassword" label="Heslo znovu" prompt="Zadejte Vaše heslo znovu" value={valuesState.confirmPassword}
                            handleChange={handleChange}/>
                <input type="submit" className="btn btn-primary mt-2" value="Registrovat se"/>
            </form>
        </div>
    );
}