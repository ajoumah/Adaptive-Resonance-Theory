# ART1 Neural Network - Adaptive Resonance Theory 1

## Overview

This project implements the **ART1 (Adaptive Resonance Theory 1)** neural network, a foundational unsupervised learning model designed to classify binary input patterns. ART1 belongs to the family of **Adaptive Resonance Theory (ART)** networks developed by Stephen Grossberg in the 1970s and 80s. These models are known for their ability to learn stable categories in response to arbitrary input streams without forgetting previously learned information — a problem known as **catastrophic forgetting** in many neural networks.

ART1 specifically deals with **binary input vectors** and adapts dynamically to recognize new patterns or reinforce existing memory categories while maintaining stability.

---

## Theory

ART1 works through two complementary processes:

- **Pattern Matching:** Incoming input patterns are compared to stored prototypes or memory categories.
- **Learning and Resonance:** If the input sufficiently matches an existing category, the network updates that category. If no suitable match exists, a new category is created.

This allows ART1 to balance between **plasticity** (learning new patterns) and **stability** (retaining old memories).

---

## Key Concepts

### Vigilance Parameter (Rho, ρ)

- The **vigilance parameter (ρ)** controls the strictness of pattern matching.
- It is a threshold that determines how closely an input pattern must match an existing category to be considered part of that category.
- If the similarity (often measured by the overlap between input and category prototype) is **below ρ**, the network will **search for or create a new category**, preventing overly broad or inaccurate category assignments.
- Conversely, a **lower ρ** means the network is more tolerant, grouping more patterns under fewer categories (more generalization).
- A **higher ρ** leads to more categories and more specific memory, as patterns must match very closely to be grouped.

In effect, ρ governs the **trade-off between generalization and specificity** in the learned memory representations.

---

## Features

- Implements core ART1 learning algorithm for binary input vectors.
- Adjustable vigilance parameter to control category formation.
- Incremental learning with stable memory retention.
- Suitable for pattern classification, clustering, and data compression tasks involving binary data.

---

## Usage

1. Prepare your binary input data (vectors of 0s and 1s).
2. Initialize the ART1 network with a chosen vigilance parameter (ρ).
3. Train the network with input patterns.
4. The network will form memory categories that classify or cluster the input patterns.

---

## Installation

Clone the repository:

```bash
git clone https://github.com/yourusername/ART1.git
cd ART1
