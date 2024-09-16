import React, { useEffect, useState, useContext } from "react";
import AuthApi from "../../AuthApi";
import AuthContext from "../../AuthContext/Context";

function userInfo() {
  document.title = "welcome";
  const authContext = useContext(AuthContext);
  const { isLoggedIn, roles } = authContext;
  const [userInfo, setUserInfo] = useState({});
  useEffect(() => {
    getUser();
  }, []);
  const getUser = async () => {
    if (isLoggedIn) {
      const info = await AuthApi.getUserInfo();
      setUserInfo(info);
    }
  };
  return (
    <section>
      {isLoggedIn ? (
        <div>
          {console.log(roles)}
          <table>
            <thead>
              <tr>
                <th>Name</th>
                <th>Email</th>
                <th>Created date</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>{userInfo?.fullName}</td>
                <td>{userInfo?.email}</td>
                <td>
                  {userInfo?.createdDate
                    ? userInfo?.createdDate.split("T")[0]
                    : ""}
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      ) : (
        <div className="warning"></div>
      )}
    </section>
  );
}
export default userInfo;
