import { useParams } from "react-router-dom";
import React, {useEffect, useContext, useState} from "react";
import AuthContext from "../../../AuthContext/Context";
import { Spinner } from "react-bootstrap";

function EmailConfirm(){
    const params = useParams();
    const authContext = useContext(AuthContext);
    const { emailConfirm } = authContext;
    const [isFail, setIsFail] = useState(false);
    const confirm = async()=>{
        if (!params.token || !params.email){
            window.location.href="/register"
        }
        const formData = new FormData();
        formData.append("token", params.token);
        formData.append("email", params.email);
        const res = await emailConfirm(formData);
        if(res){
            window.location.href="/login";
        }
        setIsFail(true);
    }
    useEffect(()=>{
        confirm();
    },[params]);
    if(isFail){
        return <h3 style={{
            display: 'flex',
            justifyContent: 'center',
            paddingTop: '40vh'
          }}>Email confirm Fail! Try again or Register again</h3>
    }
    return <h3 style={{
        display: 'flex',
        justifyContent: 'center',
        paddingTop: '40vh'
      }}>Confirming <Spinner animation="grow"/></h3>
}

export default EmailConfirm;