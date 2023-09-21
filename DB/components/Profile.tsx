import axios from "axios";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Button, Card, Image, Item } from "semantic-ui-react";

export default function Profile() {
  const [users, setUsers] = useState([]);

  useEffect(() => {
    axios
      .get("http://localhost:5152/User/Users")
      .then((response) => {
        setUsers(response.data);
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });
  }, []);

  return (
    <div>
      <Card.Group>
        {users.map((user : any) => (
          <Card key={user.userId}>
            <Image
              src={`data:image/jpeg;base64,${user.profilePicture}`} // Adjust the format as needed
              alt={user.username}
            />
            <Card.Content>
              <Card.Header>{user.username}</Card.Header>
              {/* <Card.Description>{user.email}</Card.Description> */}
            </Card.Content>
            <Item.Extra>
              <Button
                content="View"
                color="blue"
                as={Link}
                to={`/user/${user.userId}`} 
              />
            </Item.Extra>
          </Card>
        ))}
      </Card.Group>
    </div>
  );
}
