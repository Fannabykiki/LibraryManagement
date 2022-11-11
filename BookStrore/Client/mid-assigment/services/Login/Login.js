import axios from "axios";
import { useEffect, useState } from "react";

function Login(){
    const [user, setUser] = useState();
    const [password,setPassword] = useState();
    useEffect(() => {
        axios
          .post(`https://localhost:7233/api/authentication/token`,{ userName: user,password: password })
          .then((data) => {
            
          })
          .catch((error) => console.log(error));
      }, []);
}
export default Login;