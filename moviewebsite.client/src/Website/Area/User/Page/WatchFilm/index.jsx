import { useState, useRef, useEffect } from 'react';
import { useLoaderData} from "react-router-dom";
import { Container, Row, Col, Card } from 'react-bootstrap';
import { PlayFill, PauseFill, VolumeUp, VolumeMute, Fullscreen, SkipForward, SkipBackward, Pip } from 'react-bootstrap-icons';
import Slider from 'rc-slider';
import 'rc-slider/assets/index.css';
import './index.css';
import CommentSection from '../../Components/Form/CommentSection';

export default function WatchFilm() {
  const {episodes , film} = useLoaderData();
  const [currentEpisode, setCurrentEpisode] = useState(1);
  const [currentEpisodeId, setCurrentEpisodeId] = useState( Array.isArray(episodes) && episodes.length > 0 ? episodes[0].id : 0);
  const [isPlaying, setIsPlaying] = useState(false);
  const [isMuted, setIsMuted] = useState(false);
  const [volume, setVolume] = useState(1);
  const [progress, setProgress] = useState(0);
  const [currentTime, setCurrentTime] = useState(0);
  const [duration, setDuration] = useState(0);
  const [isFullScreen, setIsFullScreen] = useState(false);
  const [showControls, setShowControls] = useState(false);
  const [showVolumeControl, setShowVolumeControl] = useState(false);
  const [showTempPlayButton, setShowTempPlayButton] = useState(false);
  const [playbackSpeed, setPlaybackSpeed] = useState(1);
  const [isPictureInPicture, setIsPictureInPicture] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const videoRef = useRef(null);
  const videoContainerRef = useRef(null);
  const controlsTimeoutRef = useRef(null);
  const tempPlayButtonTimeoutRef = useRef(null);

  const handleEpisodeSelect = (episode) => {
    setCurrentEpisode(episode.episodeNumber);
    setCurrentEpisodeId(episode.id);
    videoRef.current.src = `/api/video/${episode.vidName}`;
    videoRef.current.load();
    setIsPlaying(true);
    videoRef.current.play();
  };

  const togglePlay = () => {
    if (videoRef.current.paused) {
      videoRef.current.play();
      setIsPlaying(true);
    } else {
      videoRef.current.pause();
      setIsPlaying(false);
    }
    showTempPlayPauseButton();
  };

  const handleVolumeChange = (newVolume) => {
    setVolume(newVolume);
    videoRef.current.volume = newVolume;
    setIsMuted(newVolume === 0);
  };

  const toggleMute = () => {
    if (isMuted) {
      videoRef.current.volume = volume;
      setIsMuted(false);
    } else {
      videoRef.current.volume = 0;
      setIsMuted(true);
    }
  };

  const handleProgress = () => {
    const progress = (videoRef.current.currentTime / videoRef.current.duration) * 100;
    setProgress(progress);
    setCurrentTime(videoRef.current.currentTime);
  };

  const handleLoadedMetadata = () => {
    setDuration(videoRef.current.duration);
    setIsLoading(false);
  };

  const handleProgressChange = (newProgress) => {
    const newTime = (newProgress / 100) * videoRef.current.duration;
    videoRef.current.currentTime = newTime;
    setProgress(newProgress);
    setCurrentTime(newTime);
  };

  const skipTime = (seconds) => {
    const newTime = videoRef.current.currentTime + seconds;
    videoRef.current.currentTime = newTime;
    setProgress((newTime / videoRef.current.duration) * 100);
    setCurrentTime(newTime);
  };

  const handleTimeClick = (timestamp) => {
    videoRef.current.currentTime = timestamp;
    setProgress((timestamp / videoRef.current.duration) * 100);
    setCurrentTime(timestamp);
    if (videoRef.current.paused) {
      videoRef.current.play();
      setIsPlaying(true);
    }
  };

  const toggleFullScreen = () => {
    if (!document.fullscreenElement) {
      videoContainerRef.current.requestFullscreen();
      setIsFullScreen(true);
    } else {
      document.exitFullscreen();
      setIsFullScreen(false);
    }
  };

  const togglePictureInPicture = async () => {
    try {
      if (document.pictureInPictureElement) {
        await document.exitPictureInPicture();
        setIsPictureInPicture(false);
      } else {
        await videoRef.current.requestPictureInPicture();
        setIsPictureInPicture(true);
      }
    } catch (error) {
      console.error('Failed to enter/exit Picture-in-Picture mode:', error);
    }
  };

  const formatTime = (timeInSeconds) => {
    const minutes = Math.floor(timeInSeconds / 60);
    const seconds = Math.floor(timeInSeconds % 60);
    return `${minutes}:${seconds < 10 ? '0' : ''}${seconds}`;
  };

  const handleVideoClick = () => {
    togglePlay();
    showControlsTemporarily();
    showTempPlayPauseButton();
  };

  const showControlsTemporarily = () => {
    setShowControls(true);
    if (controlsTimeoutRef.current) {
      clearTimeout(controlsTimeoutRef.current);
    }
    controlsTimeoutRef.current = setTimeout(() => {
      if (!videoRef.current.paused) {
        setShowControls(false);
      }
    }, 3000);
  };

  const showTempPlayPauseButton = () => {
    setShowTempPlayButton(true);
    if (tempPlayButtonTimeoutRef.current) {
      clearTimeout(tempPlayButtonTimeoutRef.current);
    }
    tempPlayButtonTimeoutRef.current = setTimeout(() => {
      if (!videoRef.current.paused) {
        setShowTempPlayButton(false);
      }
    }, 2000);
  };

  const handleMouseMove = () => {
    showControlsTemporarily();
  };

  const handleMouseLeave = () => {
    if (!videoRef.current.paused) {
      setShowControls(false);
    }
    setShowVolumeControl(false);
  };

  const handleSpeedChange = (e) => {
    const newSpeed = parseFloat(e.target.value);
    setPlaybackSpeed(newSpeed);
    videoRef.current.playbackRate = newSpeed;
  };

  const handleKeyDown = (e) => {
    if (e.key === 'ArrowRight') {
      skipTime(5);
    } else if (e.key === 'ArrowLeft') {
      skipTime(-5);
    }
  };

  useEffect(() => {
    const video = videoRef.current;
    video.addEventListener('loadedmetadata', handleLoadedMetadata);
    video.addEventListener('canplay', () => setIsLoading(false));
    video.addEventListener('waiting', () => setIsLoading(true));
    video.addEventListener('timeupdate', handleProgress);
    
    const handleFullscreenChange = () => {
      setIsFullScreen(!!document.fullscreenElement);
    };
    
    document.addEventListener('fullscreenchange', handleFullscreenChange);
    document.addEventListener('keydown', handleKeyDown);

    const handlePipChange = () => {
      setIsPictureInPicture(!!document.pictureInPictureElement);
    };

    video.addEventListener('enterpictureinpicture', handlePipChange);
    video.addEventListener('leavepictureinpicture', handlePipChange);

    video.src = Array.isArray(episodes) && episodes.length > 0 ? `/api/video/${episodes[0].vidName}`: "";
    video.load();
    
    return () => {
      video.removeEventListener('loadedmetadata', handleLoadedMetadata);
      video.removeEventListener('canplay', () => setIsLoading(false));
      video.removeEventListener('waiting', () => setIsLoading(true));
      video.removeEventListener('timeupdate', handleProgress);
      document.removeEventListener('fullscreenchange', handleFullscreenChange);
      document.removeEventListener('keydown', handleKeyDown);
      video.removeEventListener('enterpictureinpicture', handlePipChange);
      video.removeEventListener('leavepictureinpicture', handlePipChange);
    };
  }, []);

  return (
    <div className="movie-player">
      <Container fluid className="py-5">
        <Row className="justify-content-center">
          <Col xs={12} lg={10}>
            <h1 className="movie-title mb-4">{film.title}</h1>
            <div 
              className="video-container" 
              ref={videoContainerRef}
              onMouseMove={handleMouseMove}
              onMouseLeave={handleMouseLeave}
            >
              <video
                ref={videoRef}
                onClick={handleVideoClick}
              >
                <source type="video/mp4" />
                Your browser does not support the video tag.
              </video>
              {isLoading && (
                <div className="loading-indicator">
                  <div className="spinner"></div>
                </div>
              )}
              <div className={`video-controls ${isFullScreen ? 'fullscreen' : ''} ${showControls ? 'show' : ''}`}>
                <Slider
                  min={0}
                  max={100}
                  value={progress}
                  onChange={handleProgressChange}
                  className="progress-bar"
                />
                <div className="controls-row">
                  <button onClick={togglePlay} className="control-button">
                    {isPlaying ? <PauseFill /> : <PlayFill />}
                  </button>
                  <button onClick={() => skipTime(-5)} className="control-button">
                    <SkipBackward />
                  </button>
                  <button onClick={() => skipTime(5)} className="control-button">
                    <SkipForward />
                  </button>
                  <div 
                    className="volume-control" 
                    onMouseEnter={() => setShowVolumeControl(true)}
                    onMouseLeave={() => setShowVolumeControl(false)}
                  >
                    <button onClick={toggleMute} className="control-button">
                      {isMuted ? <VolumeMute /> : <VolumeUp />}
                    </button>
                    {showVolumeControl && (
                      <Slider
                        min={0}
                        max={1}
                        step={0.1}
                        value={isMuted ? 0 : volume}
                        onChange={handleVolumeChange}
                        className="volume-slider"
                      />
                    )}
                  </div>
                  <div className="spacer"></div>
                  <span  className="time-display">
                    {formatTime(currentTime)} / {formatTime(duration)}
                  </span>
                  <div className="speed-control-container">
                    <select 
                      value={playbackSpeed} 
                      onChange={handleSpeedChange}
                      className="speed-control"
                    >
                      <option value="0.25">0.25x</option>
                      <option value="0.5">0.5x</option>
                      <option value="1">1x</option>
                      <option value="1.5">1.5x</option>
                      <option value="2">2x</option>
                    </select>
                  </div>
                  <button 
                    onClick={togglePictureInPicture} 
                    className={`control-button ${isPictureInPicture ? 'active' : ''}`}
                  >
                    <Pip />
                  </button>
                  <button onClick={toggleFullScreen} className="control-button">
                    <Fullscreen />
                  </button>
                </div>
              </div>
              {!isLoading && (
                <button 
                  className={`temp-play-button ${showTempPlayButton || !isPlaying ? 'show' : ''}`}
                  onClick={togglePlay}
                >
                  {isPlaying ? <PauseFill size={48} /> : <PlayFill size={48} />}
                </button>
              )}
            </div>
            <div className="mt-5">
              <h3 className="episodes-title mb-3">{film.type=== "Movie"?  "Parts" :"Episodes"}</h3>
              <div className="episodes-container">
                <Row className="mx-0">
                {episodes.map((episode, index) => (
                    <Col key={index} xs={12} sm={6} md={4} lg={3} className="mb-3 px-2">
                      <Card 
                        onClick={() => handleEpisodeSelect(episode)}
                        className={`episode-card ${currentEpisode === episode.episodeNumber ? 'active' : ''}`}
                      >
                        <Card.Body>
                          <Card.Title>{film.type=== "Movie"?  "Part " :"Episode "} {episode.episodeNumber}</Card.Title>
                        </Card.Body>
                        <div className="episode-progress" />
                      </Card>
                    </Col>
                  ))}
                </Row>
              </div>
            </div>
            <CommentSection 
              episodeId={currentEpisodeId} 
              currentTime={currentTime}
              onTimeClick={handleTimeClick}
              videoPlayerRef={videoContainerRef}
            />
          </Col>
        </Row>
      </Container>
    </div>
  );
}