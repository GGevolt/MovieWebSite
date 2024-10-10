import React, { useState, useEffect, useContext } from "react";
import { Container, Row, Col, Form, Button, Alert } from "react-bootstrap";
import PropTypes from "prop-types";
import AuthContext from "../../../AuthContext/Context";
import axios from "axios";
import EmojiPicker from "emoji-picker-react";
import { Smile } from "lucide-react";
import styles from "./CommentSection.module.css";

export default function CommentSection({
  episodeId,
  currentTime,
  onTimeClick,
  videoPlayerRef,
}) {
  const authContext = useContext(AuthContext);
  const { userName } = authContext;
  const [comments, setComments] = useState([]);
  const [newComment, setNewComment] = useState("");
  const [error, setError] = useState(null);
  const [showEmojiPicker, setShowEmojiPicker] = useState(false);

  useEffect(() => {
    fetchComments();
  }, [episodeId]);

  const fetchComments = async () => {
    try {
      const response = await axios.get(`/api/comment/${episodeId}`);
      if (Array.isArray(response.data)) {
        setComments(response.data);
      } else {
        console.error("Unexpected data structure:", response.data);
        setError("Unexpected data structure received from the server");
        setComments([]);
      }
    } catch (error) {
      console.error("Error fetching comments:", error);
      setError("Failed to fetch comments. Please try again later.");
      setComments([]);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!newComment.trim() || !userName.trim() || episodeId === 0) return;

    try {
      await axios.post("/api/comment", {
        CommnentText: newComment,
        UserName: userName,
        EpisodeId: episodeId,
        Timestamp: Math.floor(currentTime),
      });
      setNewComment("");
      fetchComments();
    } catch (error) {
      console.error("Error posting comment:", error);
      setError("Failed to post comment. Please try again later.");
    }
  };

  const formatTime = (seconds) => {
    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = seconds % 60;
    return `${minutes}:${remainingSeconds.toString().padStart(2, "0")}`;
  };

  const handleEmojiClick = (emojiObject) => {
    setNewComment((prevComment) => prevComment + emojiObject.emoji);
  };

  const handleTimeClick = (seconds) => {
    onTimeClick(seconds);
    if (videoPlayerRef && videoPlayerRef.current) {
      videoPlayerRef.current.scrollIntoView({
        behavior: "smooth",
        block: "start",
      });
    }
  };

  const renderCommentText = (text) => {
    const timeRegex = /(\d{1,2}):(\d{2})/g;
    const parts = text.split(timeRegex);

    return parts.map((part, index) => {
      if (index % 3 === 0) {
        return part;
      } else if (index % 3 === 1) {
        const minutes = parseInt(part, 10);
        const seconds = parseInt(parts[index + 1], 10);
        const totalSeconds = minutes * 60 + seconds;
        return (
          <button
            key={index}
            className={styles.timeLink}
            onClick={() => handleTimeClick(totalSeconds)}
          >
            {part}:{parts[index + 1]}
          </button>
        );
      }
      return null;
    });
  };

  return (
    <Container className={styles.commentSection}>
      <h3 className={styles.commentsTitle}>Comments</h3>
      {error && <Alert variant="danger">{error}</Alert>}
      <Row>
        <Col md={8}>
          <Form onSubmit={handleSubmit}>
            <Form.Group className="mb-3 position-relative">
              <Form.Control
                as="textarea"
                rows={3}
                placeholder="Write a comment... (Use MM:SS format for timestamps)"
                value={newComment}
                onChange={(e) => setNewComment(e.target.value)}
                required
                className={styles.formControl}
              />
              <Button
                variant="outline-secondary"
                className={styles.emojiButton}
                onClick={() => setShowEmojiPicker(!showEmojiPicker)}
                aria-label="Open emoji picker"
              >
                <Smile />
              </Button>
              {showEmojiPicker && (
                <div className={styles.emojiPickerContainer}>
                  <EmojiPicker onEmojiClick={handleEmojiClick} />
                </div>
              )}
            </Form.Group>
            <Button
              variant="primary"
              type="submit"
              className={styles.submitButton}
            >
              Post Comment
            </Button>
          </Form>
        </Col>
      </Row>
      <Row className="mt-4">
        <Col>
          {Array.isArray(comments) && comments.length > 0 ? (
            comments.map((comment) => (
              <div key={comment.id} className={styles.comment}>
                <h5 className={styles.commentUser}>
                  {comment.userName}
                  {comment.timestamp && (
                    <button
                      className={styles.timestampButton}
                      onClick={() => handleTimeClick(comment.timestamp)}
                    >
                      {formatTime(comment.timestamp)}
                    </button>
                  )}
                </h5>
                <p className={styles.commentText}>
                  {renderCommentText(comment.commnentText)}
                </p>
              </div>
            ))
          ) : (
            <p className={styles.noComments}>No comments yet.</p>
          )}
        </Col>
      </Row>
    </Container>
  );
}

CommentSection.propTypes = {
  episodeId: PropTypes.number.isRequired,
  currentTime: PropTypes.number.isRequired,
  onTimeClick: PropTypes.func.isRequired,
  videoPlayerRef: PropTypes.object.isRequired,
};
