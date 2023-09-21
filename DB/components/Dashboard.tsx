import { Button, Card, Item, Segment } from "semantic-ui-react";
import { useEffect, useState } from "react";
import axios from "axios";
import { saveAs } from "file-saver";

interface UserInformation {
  userId: string;
  username: string;
  InformationId: string;
  Infoname: string;
  description: string;
  fileName: string;
}

interface Information {
  id: string;
  InformationId: string;
  file: string;
}

export default function Dashboard() {
  const [info, setInfo] = useState<UserInformation[]>([]);
  const [showCardGroup, setShowCardGroup] = useState(false);

  useEffect(() => {
    axios
      .get("http://localhost:5152/User/UsersInformation")
      .then((response) => {
        setInfo(response.data);
      })
      .catch((error) => {
        console.error("Error fetching data:", error);
      });
  }, []);

  const handleDownloadFile = (informationId: any, fileName: any) => {
    axios({
      url: `https://localhost:44306/Information/GetById?id=${informationId}`,
      method: "GET",
      responseType: "arraybuffer",
    })
      .then((response) => {
        let contentType = response.headers["content-type"];
        let blob = new Blob([response.data], {
          type: contentType,
        });
        saveAs(blob, fileName);
      })
      .catch((error) => {
        console.error("Error downloading file:", error);
      });
  };

  const handleViewButtonClick = () => {
    setShowCardGroup(true);
  };

  const handleCancelButtonClick = () => {
    setShowCardGroup(false);
  };

  return (
    <div>
      <Segment.Group>
        {info.map((activity) => (
          <Segment key={activity.userId} className="segment-width" style={{ width: "320px" }}>
            <Item.Group divided>
              <Item>
                <Item.Content>
                  <Item.Header as="a">{activity.username}</Item.Header>
                  <Item.Description>
                    <div style={{ fontSize: "16px" }}>{activity.Infoname}</div>
                    <div style={{ fontSize: "14px" }}>
                      {activity.description}
                    </div>
                  </Item.Description>
                  <Item.Extra>
                    <Button
                      content="View"
                      color="blue"
                      onClick={handleViewButtonClick}
                    />
                  </Item.Extra>
                </Item.Content>
              </Item>
            </Item.Group>
            
            {showCardGroup && (
              <Segment className="segment-width">
                <Card>
                  <Card.Content>
                    <Card.Description>This is your card content.</Card.Description>
                  </Card.Content>
                </Card>
              </Segment>
            )}
          </Segment>
        ))}
      </Segment.Group>
    </div>
  );
}
