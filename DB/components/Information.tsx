import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";
import { Button, Card } from "semantic-ui-react";
import { saveAs } from "file-saver"; 

interface Information {
  username: string;
  name: string;
  description: string;
  informationId: string; 
  fileName: string;    
}

export default function Information() {
  const { userId } = useParams();
  const [information, setInformation] = useState<Information[]>([]);
  const [loading, setLoading] = useState(true);

  const handleDownloadFile = (informationId : any, fileName: any) => {
        axios({
            url: `http://localhost:5152/Information/Download?id=${informationId}`,
            method: "GET",
            responseType: "arraybuffer",
          }).then((response) => {
            let contentType = response.headers["content-type"];
            let blob = new Blob([response.data], {
              type: contentType,
            });
            saveAs(blob, fileName);
          });
  }

  useEffect(() => {
    axios
      .get<Information[]>(`http://localhost:5152/Information/UserInformations?userId=${userId}`)
      .then((response) => {
        setInformation(response.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching user data:", error);
        setLoading(false);
      });
  }, [userId]);

  console.log(information)


  return (
    <div>
      <h2>Information</h2>
      {loading ? (
        <div className="custom-loader"></div>
      ) : (
        <Card.Group>
          {information.map((item, index) => (
            <Card key={index}>
              <Card.Content>
                <Card.Description style={{ fontSize: '16px' }}>{item.name}</Card.Description>
              </Card.Content>
              <Card.Content extra>
                <Button
                  content="Download"
                  color="blue"
                  onClick={() => handleDownloadFile(item.informationId, item.name)}
                />
              </Card.Content>
            </Card>
          ))}
        </Card.Group>
      )}
    </div>
  );
}
