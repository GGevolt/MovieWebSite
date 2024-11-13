import React, { useState, useEffect } from "react";
import { Container, Row, Col, Card } from "react-bootstrap";
import { BarChart, PieChart, LineChart } from "@mui/x-charts";
import adminApi from "../../AdminApi/adminApi";
import styles from "./Dashboard.module.css";

export default function Dashboard() {
  const [subscriptionStatus, setSubscriptionStatus] = useState([]);
  const [revenue, setRevenue] = useState([]);
  const [contentPopularity, setContentPopularity] = useState([]);
  const [userDemographics, setUserDemographics] = useState([]);
  const [genrePopularity, setGenrePopularity] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      setSubscriptionStatus(await adminApi.getSubscriptionStatusData());
      setRevenue(await adminApi.getRevenue());
      setContentPopularity(await adminApi.getContentPopularity());
      setUserDemographics(await adminApi.getUserDemographics());
      setGenrePopularity(await adminApi.getGenrePopularity());
    };
    fetchData();
  }, []);

  const renderChart = (title, chart) => (
    <Card className={styles.chart}>
      <Card.Body>
        <Card.Title>{title}</Card.Title>
        {chart}
      </Card.Body>
    </Card>
  );

  const chartProps = {
    sx: {
      "& .MuiChartsAxis-tickLabel": {
        fill: "#ffffff",
      },
      "& .MuiChartsLegend-label": {
        fill: "#ffffff",
      },
    },
  };

  return (
    <Container fluid className={styles.dashboard}>
      <h1 className={styles.title}>Dashboard</h1>
      <Row>
        <Col xs={12} md={6}>
          {renderChart(
            "Subscription Status",
            subscriptionStatus.length > 0 ? (
              <PieChart
                series={[
                  {
                    data: subscriptionStatus.map((item) => ({
                      id: item.status,
                      value: item.count,
                      label: item.status,
                    })),
                    highlightScope: { faded: "global", highlighted: "item" },
                    faded: { innerRadius: 30, additionalRadius: -30 },
                  },
                ]}
                height={300}
                {...chartProps}
                slotProps={{
                  legend: {
                    labelStyle: {
                      fill: "#ffffff",
                    },
                  },
                }}
              />
            ) : (
              <p className={styles.noData}>No data available</p>
            )
          )}
        </Col>
        <Col xs={12} md={6}>
          {renderChart(
            "Revenue",
            revenue.length > 0 ? (
              <LineChart
                xAxis={[
                  {
                    data: revenue.map((item) => new Date(item.date)),
                    scaleType: "time",
                    tickLabelStyle: {
                      fill: "#ffffff",
                    },
                  },
                ]}
                yAxis={[
                  {
                    tickLabelStyle: {
                      fill: "#ffffff",
                    },
                  },
                ]}
                series={[
                  {
                    data: revenue.map((item) => item.revenue),
                    label: "Revenue",
                  },
                ]}
                height={300}
                {...chartProps}
                slotProps={{
                  legend: {
                    labelStyle: {
                      fill: "#ffffff",
                    },
                  },
                }}
              />
            ) : (
              <p className={styles.noData}>No data available</p>
            )
          )}
        </Col>
      </Row>
      <Row>
        <Col xs={12}>
          {renderChart(
            "Content Popularity",
            contentPopularity.length > 0 ? (
              <BarChart
                xAxis={[
                  {
                    scaleType: "band",
                    data: contentPopularity.map((item) => item.title),
                    tickLabelStyle: {
                      fill: "#ffffff",
                    },
                  },
                ]}
                yAxis={[
                  {
                    tickLabelStyle: {
                      fill: "#ffffff",
                    },
                  },
                ]}
                series={[
                  {
                    data: contentPopularity.map((item) => item.viewCount),
                    label: "Views",
                  },
                  {
                    data: contentPopularity.map((item) => item.averageRating),
                    label: "Avg Rating",
                  },
                ]}
                height={400}
                {...chartProps}
                slotProps={{
                  legend: {
                    labelStyle: {
                      fill: "#ffffff",
                    },
                  },
                }}
              />
            ) : (
              <p className={styles.noData}>No data available</p>
            )
          )}
        </Col>
      </Row>
      <Row>
        <Col xs={12} md={6}>
          {renderChart(
            "User Demographics",
            userDemographics.length > 0 ? (
              <PieChart
                series={[
                  {
                    data: userDemographics.map((item) => ({
                      id: item.ageGroup,
                      value: item.count,
                      label: `${item.ageGroup} year old`,
                    })),
                    highlightScope: { faded: "global", highlighted: "item" },
                    faded: { innerRadius: 30, additionalRadius: -30 },
                  },
                ]}
                height={300}
                {...chartProps}
                slotProps={{
                  legend: {
                    labelStyle: {
                      fill: "#ffffff",
                    },
                  },
                }}
              />
            ) : (
              <p className={styles.noData}>No data available</p>
            )
          )}
        </Col>
        <Col xs={12} md={6}>
          {renderChart(
            "Genre Popularity",
            genrePopularity.length > 0 ? (
              <BarChart
                xAxis={[
                  {
                    scaleType: "band",
                    data: genrePopularity.map((item) => item.genre),
                    tickLabelStyle: {
                      fill: "#ffffff",
                    },
                  },
                ]}
                yAxis={[
                  {
                    tickLabelStyle: {
                      fill: "#ffffff",
                    },
                  },
                ]}
                series={[
                  {
                    data: genrePopularity.map((item) => item.viewCount),
                    label: "Views",
                  },
                ]}
                height={300}
                {...chartProps}
                slotProps={{
                  legend: {
                    labelStyle: {
                      fill: "#ffffff",
                    },
                  },
                }}
              />
            ) : (
              <p className={styles.noData}>No data available</p>
            )
          )}
        </Col>
      </Row>
    </Container>
  );
}
