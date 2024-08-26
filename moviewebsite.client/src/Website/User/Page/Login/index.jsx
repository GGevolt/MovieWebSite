import React,{useState} from "react";
import { Form, Button, FloatingLabel } from "react-bootstrap";
import styles from "./Login.module.css"
import { Link } from "react-router-dom";

function Login (){
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const handleLogin = (e)=>{
        e.preventDefault();
    };

    return(
        <div className={styles.Container}>
            <div className={styles.Banner}/>
            <div className={styles.Log_in_form}>
                <h1>Sign In</h1>
                <Form onSubmit={handleLogin} encType="multipart/form-data" className={styles.Form}>
                    <br/>
                    <FloatingLabel
                        label="User Name"
                        className={styles.label}
                    >
                        <Form.Control type="text" value={userName} onChange={(e) => setUserName(e.target.value)}/>
                    </FloatingLabel>
                    <br/>
                    <FloatingLabel label="Password"
                        className={styles.label}
                    >
                        <Form.Control type="password" value={password} onChange={(e) => setPassword(e.target.value)}/>
                    </FloatingLabel>
                    <br/>
                    <Button type="submit" className={styles.btn}>Sign In</Button>
                </Form>
                <hr className={styles.hr}/>
                <p>Don&apos;t have account? <Link to="/Register">Sign Up</Link></p>
            </div>
        </div>
    )
}

export default Login;